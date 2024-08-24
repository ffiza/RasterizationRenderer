using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public class Entity
    {
        public List<Triangle> Triangles { get; private set; } = new();

        /// <summary>
        /// Add a new triangle to this entity.
        /// </summary>
        /// <param name="triangle">The new triangle.</param>
        public void AddTriangle(Triangle triangle)
        {
            Triangles.Add(triangle);
        }

        /// <summary>
        /// Translate the entity by a vector <c>t</c>.
        /// </summary>
        /// <param name="t">The translation vector.</param>
        public void Translate(Vector3 t)
        {
            foreach (Triangle triangle in Triangles)
            {
                triangle.Translate(t);
            }
        }
    }
}
