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

        /// <summary>
        /// This method computes the signed distance between the plane and a
        /// point.
        /// </summary>
        /// <param name="point">The position of the point.</param>
        /// <returns></returns>
        public float ComputeSignedDistance(Vector3 point)
        {
            return Vector3.Dot(point, Normal) - Distance;
        }
    }
}
