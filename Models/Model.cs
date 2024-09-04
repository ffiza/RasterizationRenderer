using RasterizationRenderer.Utils;
using System.Drawing;
using System.Numerics;

namespace RasterizationRenderer.Models
{
    /// <summary>
    /// A class to manage a model made of triangles in world space.
    /// </summary>
    public class Model
    {
        public string Name { get; protected set; }
        public List<Triangle> Triangles { get; protected set; }
        public Color Color { get; protected set; }
        public float BoundingRadius { get; protected set; }
        public Vector3 BoundingCenter { get; protected set; }

        public Model(Color color)
        {
            Name = "Generic";
            Triangles = new List<Triangle>();
            Color = color;
        }

        /// <summary>
        /// Translate the position of the vertices of this model by a vector <c>translation</c>.
        /// </summary>
        /// <param name="translation">The translation vector.</param>
        private void Translate(Vector3 translation)
        {
            foreach (Triangle t in Triangles)
            {
                t.Translate(translation);
            }
        }

        /// <summary>
        /// Scale the position of the vertices of this model by a given factor.
        /// </summary>
        /// <param name="scale">The scale factor.</param>
        private void Scale(float scale)
        {
            foreach (Triangle t in Triangles)
            {
                t.Scale(scale);
            }
        }

        /// <summary>
        /// Rotate the position of the vertices of this model by a given quaternion.
        /// </summary>
        /// <param name="rotation">The quaternion that represents the rotation.</param>
        private void Rotate(Quaternion rotation)
        {
            foreach (Triangle t in Triangles)
            {
                t.Rotate(rotation);
            }
        }

        /// <summary>
        /// Apply (in order) all the transformations in the <c>Transform</c> of this model.
        /// </summary>
        /// <param name="transform">The <c>Transform</c> that holds all the transformations.</param>
        public void ApplyTransform(Transform transform)
        {
            Scale(transform.Scale);
            foreach (Quaternion q in transform.Rotations)
            {
                Rotate(q);
            }
            Translate(transform.Translation);
            ComputeBoundingSphere();
        }

        /// <summary>
        /// Compute the properties (center and radius) of the sphere that completely contains
        /// this <c>Model</c>.
        /// </summary>
        public void ComputeBoundingSphere()
        {
            int nVertices = 3 * Triangles.Count;
            BoundingCenter = Vector3.Zero;
            BoundingRadius = 0f;
            foreach (Triangle t in Triangles)
            {
                foreach (Vector3 v in t.Vertices)
                {
                    BoundingCenter += v / nVertices;
                }
            }
            foreach (Triangle t in Triangles)
            {
                foreach (Vector3 v in t.Vertices)
                {
                    float distance = (v - BoundingCenter).Length();
                    if (distance > BoundingRadius)
                    {
                        BoundingRadius = distance;
                    }
                }
            }
        }

        /// <summary>
        /// Set the list of <c>Triangle</c> of this <c>Model</c> with a given
        /// list.
        /// </summary>
        /// <param name="triangles">A list of <c>Triangle</c>.</param>
        public void SetTriangles(List<Triangle> triangles)
        {
            Triangles = triangles;
            ComputeBoundingSphere();
        }
    }
}