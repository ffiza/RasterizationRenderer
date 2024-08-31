using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public class Plane
    {
        public Vector3 Normal { get; private set; }
        public float Distance { get; private set; }

        public Plane(Vector3 normal, float distance)
        {
            Normal = Vector3.Normalize(normal);
            Distance = distance;
        }
    }
}
