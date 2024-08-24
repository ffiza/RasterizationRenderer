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
        /// <param name="t">The <c>Transform</c> that holds all the transformations.</param>
        public void ApplyTransform(Transform t)
        {
            Scale(t.Scale);
            foreach (Quaternion q in t.Rotations)
            {
                Rotate(q);
            }
            Translate(t.Translation);
        }
    }
}