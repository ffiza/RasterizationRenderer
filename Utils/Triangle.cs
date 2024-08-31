using System.Drawing;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

namespace RasterizationRenderer.Utils
{
    /// <summary>
    /// A class to manage triangles in world space.
    /// </summary>
    public class Triangle
    {
        public Vector3[] Vertices { get; private set; }
        public Color Color { get; private set; }
        public float BoundingRadius { get; private set; }
        public Vector3 BoundingCenter { get; private set; }

        public Triangle(Vector3 p0, Vector3 p1, Vector3 p2, Color color)
        {
            Vertices = new Vector3[3] { p0, p1, p2 };
            Color = color;
            ComputeBoundingSphere();
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

        /// <summary>
        /// Rotate the position of the vertices of this triangle by a given quaternion.
        /// </summary>
        /// <param name="rotation">The quaternion that represents the rotation.</param>
        public void Rotate(Quaternion rotation)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vector3.Transform(Vertices[i], rotation);
            }
        }

        /// <summary>
        /// Compute the properties (center and radius) of the sphere that completely contains
        /// this <c>Triangle</c>.
        /// </summary>
        private void ComputeBoundingSphere()
        {
            int nVertices = 3;
            BoundingCenter = (Vertices[0] + Vertices[1] + Vertices[2]) / (float)nVertices;
            BoundingRadius = 0f;
            foreach (Vector3 v in Vertices)
            {
                float distance = (v - BoundingCenter).Length();
                if (distance > BoundingRadius)
                {
                    BoundingRadius = distance;
                }
            }
        }
    }
}
