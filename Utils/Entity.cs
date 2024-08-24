using System.Drawing;
using RasterizationRenderer.Models;

namespace RasterizationRenderer.Utils
{
    /// <summary>
    /// A generic class to manage entities in the 3D world.
    /// </summary>
    public class Entity
    {
        private readonly string _modelName;
        private readonly Transform _transform;
        private readonly Color _color;
        public Model Model { get; private set; }

        public Entity(string modelName, Transform transform, Color color)
        {
            _modelName = modelName;
            _transform = transform;
            _color = color;
            if (_modelName == "Cube")
            {
                Model = new Cube(_color);
            }
            else
            {
                throw new Exception("Model anme not implemented.");
            }
            Model.Scale(_transform.Scale);
            Model.Translate(_transform.Translation);
        }
    }
}
