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
        public void Translate(Vector3 translation)
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
        public void Scale(float scale)
        {
            foreach (Triangle t in Triangles)
            {
                t.Scale(scale);
            }
        }
    }
}