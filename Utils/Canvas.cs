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
            Vector2 v0 = p0;
            Vector2 v1 = p1;
            if (v0.X > v1.X)
            {
                (v1, v0) = Swap.SwapVector2(v0, v1);
            }
            float a = (v1.Y - v0.Y) / (v1.X - v0.X);
            float y = v0.Y;
            for (int x = (int)v0.X; x <= v1.X; x++)
            {
                PutPixel(x, y, color);
                y += a;
            }
        }

        private void PutPixel(float x, float y, Color color)
        {
            (float newX, float newY) = Coordinates.CenterCoords(x, y, this);
            _bmp.SetPixel((int)newX, (int)newY, color);
        }
    }
}
