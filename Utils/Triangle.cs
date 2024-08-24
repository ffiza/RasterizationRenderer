using System.Drawing;
using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public class Triangle
    {
        public Vector3[] Vertices { get; private set; }
        public Color Color { get; private set; } 

        public Triangle(Vector3 p0, Vector3 p1, Vector3 p2, Color color)
        {
            Vertices = new Vector3[3] { p0, p1, p2 };
            Color = color;
        }

        /// <summary>
        /// Translate the position of the vertices of this triangle by a vector <c>t</c>.
        /// </summary>
        /// <param name="t">The translation vector.</param>
        public void Translate(Vector3 t)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i] + t;
            }
        }
    }
}
