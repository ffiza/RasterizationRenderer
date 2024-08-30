using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RasterizationRenderer.Utils
{
    public class Plane
    {
        public Vector3 Normal { get; private set; }
        public float Distance { get; private set; }

        public Plane(Vector3 normal, float distance)
        {
            Normal = normal;
            Distance = distance;
        }
    }
}
