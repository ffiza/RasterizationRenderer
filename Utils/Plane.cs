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
        /// This method computes the signed distance between this plane and a
        /// given point.
        /// </summary>
        /// <param name="point">The world space position of the point.</param>
        /// <returns>The distance between the point and this plane.</returns>
        public float ComputeSignedDistance(Vector3 point)
        {
            return Vector3.Dot(point, Normal) - Distance;
        }

        /// <summary>
        /// Computes the intersection point between this plane and a segment
        /// given by the points <c>s1</c> and <c>d2</c>. This method assumes
        /// the intersection exists; if not, it will throw an exception.
        /// </summary>
        /// <param name="s1">The first point in the segment.</param>
        /// <param name="s2">The second point in the segment.</param>
        /// <returns>The intersection point in 3D space.</returns>
        public Vector3 SegmentIntersect(Vector3 s1, Vector3 s2)
        {
            float t = (Distance - Vector3.Dot(Normal, s1)) / Vector3.Dot(Normal, s2 - s1);
            if (t < 0f || t > 1f)
            {
                throw new Exception("No intersection found between plane and segment.");
            }
            else
            {
                return s1 + t * (s2 - s1);
            }
        }
    }
}
