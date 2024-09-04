using System.Drawing;
using RasterizationRenderer.Models;

namespace RasterizationRenderer.Utils
{
    /// <summary>
    /// A generic class to manage entities in the 3D world.
    /// </summary>
    public class Entity
    {
        public string ModelName { get; private set; }
        public Transform Transform { get; private set; }
        public Color Color { get; private set; }
        public Model Model { get; private set; }

        public Entity(string modelName, Transform transform, Color color)
        {
            ModelName = modelName;
            Transform = transform;
            Color = color;
            if (ModelName == "Cube")
            {
                Model = new Cube(Color);
            }
            else
            {
                Model = new(Color);
            }
            Model.ApplyTransform(Transform);
        }
    }
}
