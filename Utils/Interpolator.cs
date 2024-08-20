using System.Numerics;

namespace RasterizationRenderer.Utils
{
    public class Interpolator
    {
        /// <summary>
        /// This method performs a generic interpolation.
        /// </summary>
        /// <param name="i0">The first value of the independ variable.</param>
        /// <param name="d0">The first value of the dependent variable.</param>
        /// <param name="i1">The second value of the independ variable.</param>
        /// <param name="d1">The second value of the dependent variable.</param>
        public static List<float> Interpolate(int i0, float d0, int i1, float d1)
        {
            List<float> values = new List<float>();
            if (i0 == i1)
            {
                values.Add(d0);
                return values;
            }
            float a = (d1 - d0) / (i1 - i0);
            float d = d0;
            for (int i = i0; i <= i1; i++)
            {
                values.Add(d);
                d += a;
            }
            return values;
        }
    }
}
