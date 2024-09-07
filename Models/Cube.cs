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

            Triangles.Add(new Triangle(new Vector3(1f, 1f, 1f), new Vector3(-1f, 1f, 1f), new Vector3(-1f, -1f, 1f), Color.SeaShell));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, 1f), new Vector3(-1f, -1f, 1f), new Vector3(1f, -1f, 1f), Color.RebeccaPurple));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(1f, 1f, 1f), new Vector3(1f, -1f, 1f), Color.SpringGreen));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(1f, -1f, 1f), new Vector3(1f, -1f, -1f), Color.Yellow));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, -1f), new Vector3(1f, 1f, -1f), new Vector3(1f, -1f, -1f), Color.Crimson));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, -1f), new Vector3(1f, -1f, -1f), new Vector3(-1f, -1f, -1f), Color.Salmon));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, 1f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, -1f, -1f), Color.AliceBlue));
            Triangles.Add(new Triangle(new Vector3(-1f, 1f, 1f), new Vector3(-1f, -1f, -1f), new Vector3(-1f, -1f, 1f), Color.BlueViolet));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, 1f, 1f), Color.Peru));
            Triangles.Add(new Triangle(new Vector3(1f, 1f, -1f), new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f), Color.Pink));
            Triangles.Add(new Triangle(new Vector3(-1f, -1f, 1f), new Vector3(-1f, -1f, -1f), new Vector3(1f, -1f, -1f), Color.Purple));
            Triangles.Add(new Triangle(new Vector3(-1f, -1f, 1f), new Vector3(1f, -1f, -1f), new Vector3(1f, -1f, 1f), Color.Green));
        }
    }
}
