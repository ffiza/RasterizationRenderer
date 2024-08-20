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
            canvas.DrawLine(new Vector2(-200, -100), new Vector2(240, 120), Color.Black);
            canvas.DrawLine(new Vector2(-50, -200), new Vector2(60, 240), Color.Black);
            canvas.DrawWireframeTriangle(new Vector2(-200, -250), new Vector2(200, 50), new Vector2(20, 250), Color.Red);
            canvas.SaveToFile("image.png");
        }
    }
}