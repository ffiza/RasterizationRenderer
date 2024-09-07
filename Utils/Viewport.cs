using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public class Viewport
    {
        public float DistanceToCamera { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        public List<Plane> ClippingPlanes { get; private set; }
        public Vector3 CameraPosition { get; private set; }

        public Viewport(float distanceToCamera, float width, float height, List<Plane> clippingPlanes)
        {
            DistanceToCamera = distanceToCamera;
            Width = width;
            Height = height;
            ClippingPlanes = clippingPlanes;
            CameraPosition = Vector3.Zero;
        }
    }
}
