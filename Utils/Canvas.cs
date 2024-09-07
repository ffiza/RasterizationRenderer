using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public class Canvas
    {
        private readonly Bitmap _bmp;
        private readonly int _width;
        private readonly int _height;

        public int Width { get => _width; }
        public int Height { get => _height; }
        private float[,] _depthBuffer;

        public Canvas(int width, int height, Color color)
        {
            _width = width;
            _height = height;
            _bmp = new Bitmap(width, height);
            _depthBuffer = Tools.GetArrayOfFloat(_width, _height, 0f);
            Clear(color);
        }

        /// <summary>
        /// This method sets all the pixels of the canvas to the given color.
        /// </summary>
        /// <param name="color">The new color of the canvas.</param>
        private void Clear(Color color)
        {
            Graphics g = Graphics.FromImage(_bmp);
            g.Clear(color);
        }

        /// <summary>
        /// This method draws a single pixel in the given position with the given color.
        /// Internally, there's a convenient coordinate transformation for the top left corner
        /// to the center of the canvas.
        /// </summary>
        /// <param name="x">The position in the x-axis.</param>
        /// <param name="y">The posiiton in the y-axis.</param>
        /// <param name="color">The color of the pixel.</param>
        private void PutPixel(float x, float y, Color color)
        {
            (float newX, float newY) = Coordinates.Transform(x, y, this);
            if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
            {
                _bmp.SetPixel((int)newX, (int)newY, color);
            }
        }

        /// <summary>
        /// This method saves the canvas to a PNG file.
        /// </summary>
        /// <param name="filename">The name of the output file.</param>
        public void SaveToPNG(string filename)
        {
            if (File.Exists(filename))
            {
                Console.WriteLine("Existing file found. Deleting.");
                File.SetAttributes(filename, FileAttributes.Normal);
                File.Delete(filename);
            }
            _bmp.Save(filename, ImageFormat.Png);
            _bmp.Dispose();
        }

        /// <summary>
        /// This method draws a single point (pixel) in the canvas.
        /// </summary>
        /// <param name="p">The position of the pixel.</param>
        /// <param name="color">The color of the pixel.</param>
        public void DrawPoint(Vector2 p, Color color)
        {
            PutPixel(p.X, p.Y, color);
        }

        /// <summary>
        /// This method draws a colored line from point <c>p0</c> to point <c>p1</c>.
        /// Note that which is the start and which is the end of the line is
        /// irrelevant.
        /// </summary>
        /// <param name="p0">The start point of the line.</param>
        /// <param name="p1">The end point of the line.</param>
        /// <param name="color">The color of the line.</param>
        public void DrawLine(Vector2 p0, Vector2 p1, Color color)
        {
            if (MathF.Abs(p1.X - p0.X) > MathF.Abs(p1.Y - p0.Y))
            {
                if (p0.X > p1.X)
                {
                    (p0, p1) = (p1, p0);
                }
                List<float> ys = Interpolator.Interpolate((int)p0.X, p0.Y, (int)p1.X, p1.Y);
                for (int x = (int)p0.X; x <= p1.X; x++)
                {
                    PutPixel(x, ys[x - (int)p0.X], color);
                }
            }
            else
            {
                if (p0.Y > p1.Y)
                {
                    (p0, p1) = (p1, p0);
                }
                List<float> xs = Interpolator.Interpolate((int)p0.Y, p0.X, (int)p1.Y, p1.X);
                for (int y = (int)p0.Y; y <= p1.Y; y++)
                {
                    PutPixel(xs[y - (int)p0.Y], y, color);
                }
            }
        }

        /// <summary>
        /// This method draws a colored wireframe triangle of vertices <c>p0</c>, <c>p1</c>, and <c>p2</c>.
        /// </summary>
        /// <param name="p0">A vertex of the triangle.</param>
        /// <param name="p1">A vertex of the triangle.</param>
        /// <param name="p2">A vertex of the triangle.</param>
        /// <param name="color">The color of the triangle.</param>
        public void DrawWireframeTriangle(Vector2 p0, Vector2 p1, Vector2 p2, Color color)
        {
            DrawLine(p0, p1, color);
            DrawLine(p1, p2, color);
            DrawLine(p2, p0, color);
        }

        /// <summary>
        /// This method draws a colored filled triangle of vertices <c>p0</c>, <c>p1</c>, and <c>p2</c>.
        /// The values of world space z, <c>z0</c>, <c>z1</c>, and <c>z2</c> are used for depth buffering.
        /// </summary>
        /// <param name="p0">A vertex of the triangle.</param>
        /// <param name="p1">A vertex of the triangle.</param>
        /// <param name="p2">A vertex of the triangle.</param>
        /// <param name="z0">The world space z coordinate of point <c>p0</c>.</param>
        /// <param name="z1">The world space z coordinate of point <c>p1</c>.</param>
        /// <param name="z2">The world space z coordinate of point <c>p2</c>.</param>
        /// <param name="color">The color of the triangle.</param>
        public void DrawFilledTriangle(Vector2 p0, Vector2 p1, Vector2 p2, float z0, float z1, float z2, Color color)
        {
            // Order vertices from bottom (p0) to top (p2).
            if (p1.Y < p0.Y) { (p0, p1) = (p1, p0); (z0, z1) = (z1, z0); }
            if (p2.Y < p0.Y) { (p2, p0) = (p0, p2); (z2, z0) = (z0, z2); }
            if (p2.Y < p1.Y) { (p2, p1) = (p1, p2); (z2, z1) = (z1, z2); }

            // Compute values for the three sides of the triangle.
            List<float> x01 = Interpolator.Interpolate((int)p0.Y, p0.X, (int)p1.Y, p1.X);
            List<float> x12 = Interpolator.Interpolate((int)p1.Y, p1.X, (int)p2.Y, p2.X);
            List<float> x02 = Interpolator.Interpolate((int)p0.Y, p0.X, (int)p2.Y, p2.X);

            List<float> z01 = Interpolator.Interpolate((int)p0.Y, z0, (int)p1.Y, z1);
            List<float> z12 = Interpolator.Interpolate((int)p1.Y, z1, (int)p2.Y, z2);
            List<float> z02 = Interpolator.Interpolate((int)p0.Y, z0, (int)p2.Y, z2);

            // Remove repeated value.
            x01.RemoveAt(x01.Count - 1);
            z01.RemoveAt(z01.Count - 1);

            // Concatenate lists to find the values of the long side of the triangle.
            List<float> x012 = (x01.Concat(x12)).ToList();
            List<float> z012 = (z01.Concat(z12)).ToList();

            // Determine which is the left side and which is the right side by comparing
            // the values at the middle height.
            List<float> xLeft, xRight;
            List<float> zLeft, zRight;
            int m = (int)MathF.Floor(x012.Count / 2);
            if (x02[m] < x012[m])
            {
                xLeft = x02;
                xRight = x012;
                zLeft = z02;
                zRight = z012;
            }
            else
            {
                xLeft = x012;
                xRight = x02;
                zLeft = z012;
                zRight = z02;
            }

            // Draw all the lines. This part does not use the DrawLine method because
            // all are horizontal lines.
            for (int y = (int)p0.Y;  y <= p2.Y; y++)
            {
                int xLeftPoint = (int)xLeft[y - (int)p0.Y];
                int xRightPoint = (int)xRight[y - (int)p0.Y];
                List<float> zSegment = Interpolator.Interpolate(xLeftPoint, zLeft[y - (int)p0.Y], xRightPoint, zRight[y - (int)p0.Y]);
                for (int x = xLeftPoint; x <= xRightPoint; x++)
                {
                    float z = zSegment[x - xLeftPoint];
                    (float bufferX, float bufferY) = Coordinates.Transform(x, y, this);
                    if (1 / z > _depthBuffer[(int)bufferX, (int)bufferY])
                    {
                        PutPixel(x, y, color);
                        _depthBuffer[(int)bufferX, (int)bufferY] = 1 / z;
                    }
                }
            }
        }

        /// <summary>
        /// This method draws a colored filled triangle of vertices <c>p0</c>, <c>p1</c>, and <c>p2</c>, shaded
        /// with intensity values <c>h0</c>, <c>h1</c>, and <c>h2</c> for each vertex.
        /// </summary>
        /// <param name="p0">A vertex of the triangle.</param>
        /// <param name="p1">A vertex of the triangle.</param>
        /// <param name="p2">A vertex of the triangle.</param>
        /// <param name="p0">The color intensity of vertex <c>p0</c> of the triangle.</param>
        /// <param name="p1">The color intensity of vertex <c>p1</c> of the triangle.</param>
        /// <param name="p2">The color intensity of vertex <c>p2</c> of the triangle.</param>
        /// <param name="color">The color of the triangle.</param>
        public void DrawShadedTriangle(Vector2 p0, Vector2 p1, Vector2 p2, float h0, float h1, float h2, Color color)
        {
            // Check all intensities are between 0 and 1.
            if (h0 < 0f || h0 > 1f) { throw new ArgumentOutOfRangeException(nameof(h0), "Intensities must be between 0 and 1."); }
            if (h1 < 0f || h1 > 1f) { throw new ArgumentOutOfRangeException(nameof(h1), "Intensities must be between 0 and 1."); }
            if (h2 < 0f || h2 > 1f) { throw new ArgumentOutOfRangeException(nameof(h2), "Intensities must be between 0 and 1."); }

            // Order vertices from bottom (p0) to top (p2) and the corresponding intensities.
            if (p1.Y < p0.Y) { (p0, p1) = (p1, p0); (h0, h1) = (h1, h0); }
            if (p2.Y < p0.Y) { (p2, p0) = (p0, p2); (h2, h0) = (h0, h2); }
            if (p2.Y < p1.Y) { (p2, p1) = (p1, p2); (h2, h1) = (h1, h2); }

            // Compute values of x and h for the three sides of the triangle.
            List<float> x01 = Interpolator.Interpolate((int)p0.Y, p0.X, (int)p1.Y, p1.X);
            List<float> h01 = Interpolator.Interpolate((int)p0.Y, h0, (int)p1.Y, h1);
            List<float> x12 = Interpolator.Interpolate((int)p1.Y, p1.X, (int)p2.Y, p2.X);
            List<float> h12 = Interpolator.Interpolate((int)p1.Y, h1, (int)p2.Y, h2);
            List<float> x02 = Interpolator.Interpolate((int)p0.Y, p0.X, (int)p2.Y, p2.X);
            List<float> h02 = Interpolator.Interpolate((int)p0.Y, h0, (int)p2.Y, h2);

            // Remove repeated value in x01.
            x01.RemoveAt(x01.Count - 1);

            // Concatenate lists to find the x values of the long side of the triangle.
            List<float> x012 = (x01.Concat(x12)).ToList();

            // Remove repeated value in h01.
            h01.RemoveAt(h01.Count - 1);

            // Concatenate lists to find the h values of the long side of the triangle.
            List<float> h012 = (h01.Concat(h12)).ToList();

            // Determine which is the left side and which is the right side by comparing
            // the x values at the middle height.
            List<float> xLeft, xRight;
            List<float> hLeft, hRight;
            int m = (int)MathF.Floor(x012.Count / 2);
            if (x02[m] < x012[m])
            {
                xLeft = x02;
                hLeft = h02;
                xRight = x012;
                hRight = h012;
            }
            else
            {
                xLeft = x012;
                hLeft = h012;
                xRight = x02;
                hRight = h02;
            }

            // Draw all the lines. This part does not use the DrawLine method because
            // all are horizontal lines.
            for (int y = (int)p0.Y; y <= p2.Y; y++)
            {
                int xLeftPoint = (int)xLeft[y - (int)p0.Y];
                int xRightPoint = (int)xRight[y - (int)p0.Y];
                List<float> hSegment = Interpolator.Interpolate(xLeftPoint, hLeft[y - (int)p0.Y], xRightPoint, hRight[y - (int)p0.Y]);
                for (int x = xLeftPoint; x <= xRightPoint; x++)
                {
                    float h = hSegment[x - xLeftPoint];
                    Color shadedColor = Color.FromArgb((int)(color.R * h), (int)(color.G * h), (int)(color.B * h));
                    PutPixel(x, y, shadedColor);
                }
            }
        }
    }
}
