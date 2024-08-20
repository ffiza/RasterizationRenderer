using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public class Swap
    {
        public static (Vector2, Vector2) SwapVector2(Vector2 p0, Vector2 p1)
        {
            return (p1, p0);
        }
    }
}
