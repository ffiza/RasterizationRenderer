using System.Drawing;
using System.Numerics;

namespace RasterizationRenderer.Utils
{
    /// <summary>
    /// A class to manage triangles in world space.
    /// </summary>
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
        /// Translate the position of the vertices of this triangle by a vector <c>translation</c>.
        /// </summary>
        /// <param name="translation">The translation vector.</param>
        public void Translate(Vector3 translation)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i] + translation;
            }
        }

        /// <summary>
        /// Scale the position of the vertices of this triangle by a given factor.
        /// </summary>
        /// <param name="scale">The scale factor.</param>
        public void Scale(float scale)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] *= scale;
            }
        }
    }
}
