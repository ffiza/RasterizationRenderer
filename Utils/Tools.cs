namespace RasterizationRenderer.Utils
{
    public static class Tools
    {
        /// <summary>
        /// Returns and array of size <c>[rows, cols]</c> filled with <c>value</c>.
        /// </summary>
        /// <param name="rows">The number of rows in the array.</param>
        /// <param name="cols">The number of columns in the array.</param>
        /// <param name="value">The value to fill the array with.</param>
        /// <returns>An array filled with <c>value</c>.</returns>
        public static float[,] GetArrayOfFloat(int rows, int cols, float value)
        {
            float[,] arr = new float[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr[i, j] = value;
                }
            }
            return arr;
        }
    }
}
