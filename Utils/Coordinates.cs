namespace RasterizationRenderer.Utils
{
    public class Coordinates
    {
        public Coordinates() { }

        public static (float, float) CenterCoords(float x, float y, Canvas canvas)
        {
            float newX = x + canvas.Width / 2;
            float newY = -y + canvas.Height / 2;
            return (newX, newY);
        }
    }
}