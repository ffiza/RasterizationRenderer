using System.Numerics;

namespace RasterizationRenderer.Utils
{
    /// <summary>
    /// A class to manage a transform, with a scale and a translation vector.
    /// </summary>
    public class Transform
    {
        public float Scale { get; private set; }
        public Vector3 Translation { get; private set; }

        public Transform(float scale, Vector3 translation)
        {
            Scale = scale;
            Translation = translation;
        }
    }
}