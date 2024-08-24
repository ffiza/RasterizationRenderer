using System.Drawing;
using RasterizationRenderer.Utils;

namespace RasterizationRenderer
{
    class Program
    {
        static void Main()
        {
            Canvas canvas = new(1000, 1000, Color.White);
            Scene scene = Scene.FromJSON("C:\\Users\\Usuario\\Documents\\Repositories\\RasterizationRenderer\\Scenes\\ThreeCubes.json");
            scene.Render(canvas);
            canvas.SaveToPNG("image.png");
        }
    }
}