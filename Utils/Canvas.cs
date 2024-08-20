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

        public Canvas(int width, int height, Color color)
        {
            _width = width;
            _height = height;
            _bmp = new Bitmap(width, height);
            SetBackgroundColor(color);
        }

        private void SetBackgroundColor(Color color)
        {
            Graphics g = Graphics.FromImage(_bmp);
            g.Clear(color);
        }

        public void SaveToFile(string filename)
        {
            if (File.Exists(filename))
            {
                Console.WriteLine("File exists.");
                File.Delete(filename);
            }
            _bmp.Save(filename, ImageFormat.Png);
            _bmp.Dispose();
        }

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

        public void DrawWireframeTriangle(Vector2 p0, Vector2 p1, Vector2 p2, Color color)
        {
            DrawLine(p0, p1, color);
            DrawLine(p1, p2, color);
            DrawLine(p2, p0, color);
        }

        public void DrawFilledTriangle(Vector2 p0, Vector2 p1, Vector2 p2, Color color)
        {
            // Order vertices from bottom (p0) to top (p2)
            if (p1.Y < p0.Y) { (p0, p1) = (p1, p0); }
            if (p2.Y < p0.Y) { (p2, p0) = (p0, p2); }
            if (p2.Y < p1.Y) { (p2, p1) = (p1, p2); }

            // Compute values of x for the three sides of the triangle
            List<float> x01 = Interpolator.Interpolate((int)p0.Y, p0.X, (int)p1.Y, p1.X);
            List<float> x12 = Interpolator.Interpolate((int)p1.Y, p1.X, (int)p2.Y, p2.X);
            List<float> x02 = Interpolator.Interpolate((int)p0.Y, p0.X, (int)p2.Y, p2.X);

            // Remove repeated value in `x01`
            x01.RemoveAt(x01.Count - 1);

            // Concatenate lists to find the x values of the 'long' side of the triangle
            List<float> x012 = (x01.Concat(x12)).ToList();

            // Determine which is the left side and which is the right side by comparing
            // the x values at the middle height
            List<float> xLeft, xRight;
            int m = (int)MathF.Floor(x012.Count / 2);
            if (x02[m] < x012[m])
            {
                xLeft = x02;
                xRight = x012;
            }
            else
            {
                xLeft = x012;
                xRight = x02;
            }

            // Draw all the lines. I'm not using the `DrawLine` method here because this is simpler,
            // all are horizontal lines
            for (int y = (int)p0.Y;  y <= p2.Y; y++)
            {
                for (int x = (int)xLeft[y - (int)p0.Y]; x <= xRight[y - (int)p0.Y]; x++)
                {
                    PutPixel(x, y, color);
                }
            }
        }

        private void PutPixel(float x, float y, Color color)
        {
            (float newX, float newY) = Coordinates.CenterCoords(x, y, this);
            _bmp.SetPixel((int)newX, (int)newY, color);
        }
    }
}
