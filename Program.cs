using System.Drawing;
using System.Numerics;
using RasterizationRenderer.Utils;

namespace RasterizationRenderer
{
    class Program
    {
        static void Main()
        {
            Canvas canvas = new(600, 600, Color.White);
            Viewport viewport = new(1f, 1f, 1f);

            #region DrawCube
            Vector3 vAf = new(-2f, -0.5f, 5f);
            Vector3 vBf = new(-2f, 0.5f, 5f);
            Vector3 vCf = new(-1f, 0.5f, 5f);
            Vector3 vDf = new(-1f, -0.5f, 5f);

            Vector3 vAb = new(-2f, -0.5f, 6f);
            Vector3 vBb = new(-2f, 0.5f, 6f);
            Vector3 vCb = new(-1f, 0.5f, 6f);
            Vector3 vDb = new(-1f, -0.5f, 6f);

            canvas.DrawLine(WorldToCanvas(vAf, viewport, canvas), WorldToCanvas(vBf, viewport, canvas), Color.Blue);
            canvas.DrawLine(WorldToCanvas(vBf, viewport, canvas), WorldToCanvas(vCf, viewport, canvas), Color.Blue);
            canvas.DrawLine(WorldToCanvas(vCf, viewport, canvas), WorldToCanvas(vDf, viewport, canvas), Color.Blue);
            canvas.DrawLine(WorldToCanvas(vDf, viewport, canvas), WorldToCanvas(vAf, viewport, canvas), Color.Blue);

            canvas.DrawLine(WorldToCanvas(vAb, viewport, canvas), WorldToCanvas(vBb, viewport, canvas), Color.Red);
            canvas.DrawLine(WorldToCanvas(vBb, viewport, canvas), WorldToCanvas(vCb, viewport, canvas), Color.Red);
            canvas.DrawLine(WorldToCanvas(vCb, viewport, canvas), WorldToCanvas(vDb, viewport, canvas), Color.Red);
            canvas.DrawLine(WorldToCanvas(vDb, viewport, canvas), WorldToCanvas(vAb, viewport, canvas), Color.Red);

            canvas.DrawLine(WorldToCanvas(vAf, viewport, canvas), WorldToCanvas(vAb, viewport, canvas), Color.Green);
            canvas.DrawLine(WorldToCanvas(vBf, viewport, canvas), WorldToCanvas(vBb, viewport, canvas), Color.Green);
            canvas.DrawLine(WorldToCanvas(vCf, viewport, canvas), WorldToCanvas(vCb, viewport, canvas), Color.Green);
            canvas.DrawLine(WorldToCanvas(vDf, viewport, canvas), WorldToCanvas(vDb, viewport, canvas), Color.Green);
            #endregion

            canvas.SaveToPNG("image.png");
        }

        public static Vector2 WorldToViewport(Vector3 worldPos, Viewport viewport)
        {
            return new(worldPos.X * viewport.DistanceToCamera / worldPos.Z, worldPos.Y * viewport.DistanceToCamera / worldPos.Z);
        }

        public static Vector2 ViewportToCanvas(Vector2 viewportPos, Viewport viewport, Canvas canvas)
        {
            return new Vector2((int)(viewportPos.X * canvas.Width / viewport.Width), (int)(viewportPos.Y * canvas.Height / viewport.Height));
        }

        public static Vector2 WorldToCanvas(Vector3 worldPos, Viewport viewport, Canvas canvas)
        {
            return ViewportToCanvas(WorldToViewport(worldPos, viewport), viewport, canvas);
        }
    }
}