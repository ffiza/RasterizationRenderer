using System.Drawing;
using RasterizationRenderer.Utils;

namespace RasterizationRenderer
{
    class Program
    {
        static void Main()
        {
            Canvas canvas = new(1000, 1000, Color.White);
            Scene scene = Scene.FromJSON("Scenes\\ThreeCubes.json");
            scene.Render(canvas);
            canvas.SaveToPNG("image.png");
        }
    }
}