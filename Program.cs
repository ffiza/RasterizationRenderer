using System.Drawing;
using System.Numerics;
using RasterizationRenderer.Utils;

namespace RasterizationRenderer
{
    class Program
    {
        static void Main()
        {
            Canvas canvas = new(600, 600, Color.White);
            Viewport viewport = new(1f, 1f, 1f);
            Scene scene = new(viewport);

            Entity entity = new();
            entity.AddTriangle(new(new Vector3(1f, 1f, 1f), new Vector3(-1f, 1f, 1f), new Vector3(-1f, -1f, 1f), Color.Red));
            entity.AddTriangle(new(new Vector3(1f, 1f, 1f), new Vector3(-1f, -1f, 1f), new Vector3(1f, -1f, 1f), Color.Red));
            entity.AddTriangle(new(new Vector3(1f, 1f, -1f), new Vector3(1f, 1f, 1f), new Vector3(1f, -1f, 1f), Color.Green));
            entity.AddTriangle(new(new Vector3(1f, 1f, -1f), new Vector3(1f, -1f, 1f), new Vector3(1f, -1f, -1f), Color.Green));
            entity.AddTriangle(new(new Vector3(-1f, 1f, -1f), new Vector3(1f, 1f, -1f), new Vector3(1f, -1f, -1f), Color.Blue));
            entity.AddTriangle(new(new Vector3(-1f, 1f, -1f), new Vector3(1f, -1f, -1f), new Vector3(-1f, -1f, -1f), Color.Blue));
            entity.AddTriangle(new(new Vector3(-1f, 1f, 1f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, -1f, -1f), Color.Yellow));
            entity.AddTriangle(new(new Vector3(-1f, 1f, 1f), new Vector3(-1f, -1f, -1f), new Vector3(-1f, -1f, 1f), Color.Yellow));
            entity.AddTriangle(new(new Vector3(1f, 1f, -1f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, 1f, 1f), Color.Purple));
            entity.AddTriangle(new(new Vector3(1f, 1f, -1f), new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f), Color.Purple));
            entity.AddTriangle(new(new Vector3(-1f, -1f, 1f), new Vector3(-1f, -1f, -1f), new Vector3(1f, -1f, -1f), Color.Cyan));
            entity.AddTriangle(new(new Vector3(-1f, -1f, 1f), new Vector3(1f, -1f, -1f), new Vector3(1f, -1f, 1f), Color.Cyan));
            entity.Translate(new Vector3(-1.5f, 0f, 7f));
            scene.AddEntity(entity);


            scene.Render(canvas);
            canvas.SaveToPNG("image.png");
        }
    }
}