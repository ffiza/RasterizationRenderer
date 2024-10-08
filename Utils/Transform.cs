﻿using System.Numerics;

namespace RasterizationRenderer.Utils
{
    /// <summary>
    /// A class to manage a transform, with a scale and a translation vector.
    /// </summary>
    public class Transform
    {
        public Vector3 Scale { get; private set; }
        public Vector3 Translation { get; private set; }
        public List<Quaternion> Rotations { get; private set; }

        public Transform(Vector3 scale, List<Vector3> rotations, Vector3 translation)
        {
            Scale = scale;
            Translation = translation;
            
            // Transform rotation angles to quaternions.
            Rotations = new();
            foreach (Vector3 v  in rotations)
            {
                if (v.Length() <= 0.01f)
                {
                    Rotations.Add(Quaternion.Identity);
                }
                else
                {
                    Rotations.Add(Quaternion.CreateFromAxisAngle(Vector3.Normalize(v), (MathF.PI / 180f) * v.Length()));
                }
            }
        }
    }
}