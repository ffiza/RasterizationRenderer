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
        /// </summary>
        {
            {
                {
                }
                else if (d2 < 0f)
                {
                    Vector3 new_d0 = plane.SegmentIntersect(triangle.Vertices[0], triangle.Vertices[2]);
                    Vector3 new_d1 = plane.SegmentIntersect(triangle.Vertices[1], triangle.Vertices[2]);
                    clippingResult.Add(new Triangle(triangle.Vertices[0], triangle.Vertices[1], new_d0, triangle.Color));
                    clippingResult.Add(new Triangle(new_d0, triangle.Vertices[1], new_d1, triangle.Color));
                }
            }
            return clippingResult;
        }

        /// <summary>
        /// Clip a list of <c>Triangle</c> against a given <c>Plane</c>.
        /// </summary>
        /// <param name="triangles">A list of <c>Triangle</c> to clip.</param>
        /// <param name="plane">The clipping plane.</param>
        /// <returns>A list of <c>Triangle</c> the results from the clipping process.</returns>
        public static List<Triangle> ClipTrianglesAgainstPlane(List<Triangle> triangles, Plane plane)
        {
            List<Triangle> clippingResult = new List<Triangle>();
            foreach (Triangle triangle in triangles)
            {
                List<Triangle> thisClippingResult = triangle.ClipAgainstPlane(plane);
                clippingResult.AddRange(thisClippingResult);
            }
            return clippingResult;
        }
    }
}
