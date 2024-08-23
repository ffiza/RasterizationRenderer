namespace RasterizationRenderer.Utils
{
    public class Coordinates
    {
        public Coordinates() { }

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
    }
}