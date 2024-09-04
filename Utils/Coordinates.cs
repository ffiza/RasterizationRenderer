using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public static class Coordinates
    {
        /// <summary>
        /// This method performs a convenient transformation of coordinates, from center of the canvas with x
        /// pointing to the right and y poiting up to the top left corner of the canvas with x pointing to
        /// the right and y pointing down.
        /// </summary>
        /// <param name="x">The value of the x coordinate.</param>
        /// <param name="y">The value of the y coordinate.</param>
        /// <param name="canvas">The canvas instance.</param>
        public static (float, float) Transform(float x, float y, Canvas canvas)
        {
            float newX = x + canvas.Width / 2;
            float newY = -y + canvas.Height / 2;
            return (newX, newY);
        }

        /// <summary>
        /// Project 3D world coordinates to 2D viewport coordinates.
        /// </summary>
        /// <param name="worldPos">The 3D coordinates in world space.</param>
        /// <param name="viewport">The Viewport instance.</param>
        /// <returns></returns>
        public static Vector2 WorldToViewport(Vector3 worldPos, Viewport viewport)
        {
            return new(worldPos.X * viewport.DistanceToCamera / worldPos.Z, worldPos.Y * viewport.DistanceToCamera / worldPos.Z);
        }

        /// <summary>
        /// Transform 2D float viewport coordinates to 2D int canvas coordinates.
        /// </summary>
        /// <param name="viewportPos">The 2D coordinates in viewport space.</param>
        /// <param name="viewport">The Viewport instance.</param>
        /// <param name="canvas">The Canvas instance.</param>
        /// <returns></returns>
        public static Vector2 ViewportToCanvas(Vector2 viewportPos, Viewport viewport, Canvas canvas)
        {
            return new Vector2((int)(viewportPos.X * canvas.Width / viewport.Width), (int)(viewportPos.Y * canvas.Height / viewport.Height));
        }

        /// <summary>
        /// Project and transform 3D world space coordinates into 2D canvas coordinates.
        /// </summary>
        /// <param name="worldPos">The 3D coordinates in world space.</param>
        /// <param name="viewport">The Viewport instance.</param>
        /// <param name="canvas">The Canvas instance.</param>
        /// <returns></returns>
        public static Vector2 WorldToCanvas(Vector3 worldPos, Viewport viewport, Canvas canvas)
        {
            return ViewportToCanvas(WorldToViewport(worldPos, viewport), viewport, canvas);
        }
    }
}