namespace RasterizationRenderer.Utils
{
    public class Viewport
    {
        public float DistanceToCamera { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        public Viewport(float distanceToCamera, float width, float height)
        {
            DistanceToCamera = distanceToCamera;
            Width = width;
            Height = height;
        }
    }
}
