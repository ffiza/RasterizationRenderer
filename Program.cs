using System.Drawing;
using System.Numerics;
using RasterizationRenderer.Utils;

namespace RasterizationRenderer
{
    class Program
    {
        static void Main()
        {
            const int WIDTH = 600;
            const int HEIGHT = 600;
            Color BACKGROUND_COLOR = Color.White;

            Canvas canvas = new(WIDTH, HEIGHT, BACKGROUND_COLOR);
            canvas.DrawLine(new Vector2(-50, -200), new Vector2(60, 240), Color.Black);
            canvas.SaveToFile("image.png");
        }
    }
}