using System.Drawing;
using RasterizationRenderer.Utils;

namespace RasterizationRenderer
{
    class Program
    {
        static void Main(string[] args)
        { 
            Canvas canvas = new(1000, 1000, Color.White);
            Scene scene = Scene.FromJSON("C:\\Users\\Usuario\\Documents\\Repositories\\RasterizationRenderer\\Scenes\\" + args[0] + ".json");
            scene.Render(canvas);
            canvas.SaveToPNG("Renders\\" + args[0] + ".png");
        }
    }
}