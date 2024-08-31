using System.Drawing;
using System.Numerics;
using RasterizationRenderer.Utils;

namespace RasterizationRenderer.Models
{
    /// <summary>
    /// A class to represent a cube centered at (0, 0, 0) and of side length 2.0.
    /// </summary>
    public class Cube : Model
    {
        public Cube(Color color) : base(color)
        {
            Name = "Cube";

            Triangles.Add(new Triangle(new Vector3(1f, 1f, 1f), new Vector3(-1f, 1f, 1f), new Vector3(-1f, -1f, 1f), color));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, 1f), new Vector3(-1f, -1f, 1f), new Vector3(1f, -1f, 1f), color));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(1f, 1f, 1f), new Vector3(1f, -1f, 1f), color));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(1f, -1f, 1f), new Vector3(1f, -1f, -1f), color));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, -1f), new Vector3(1f, 1f, -1f), new Vector3(1f, -1f, -1f), color));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, -1f), new Vector3(1f, -1f, -1f), new Vector3(-1f, -1f, -1f), color));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, 1f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, -1f, -1f), color));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, 1f), new Vector3(-1f, -1f, -1f), new Vector3(-1f, -1f, 1f), color));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, 1f, 1f), color));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f), color));
            Triangles.Add(new Triangle(new Vector3(-1f, -1f, 1f), new Vector3(-1f, -1f, -1f), new Vector3(1f, -1f, -1f), color));
            Triangles.Add(new Triangle(new Vector3(-1f, -1f, 1f), new Vector3(1f, -1f, -1f), new Vector3(1f, -1f, 1f), color));

            ComputeBoundingSphere();
        }
    }
}
