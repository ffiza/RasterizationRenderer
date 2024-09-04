using System.Drawing;
using RasterizationRenderer.Models;

namespace RasterizationRenderer.Utils
{
    /// <summary>
    /// A generic class to manage entities in the 3D world.
    /// </summary>
    public class Entity
    {
        public string ModelName { get; private set; }
        public Transform Transform { get; private set; }
        public Color Color { get; private set; }
        public Model Model { get; private set; }

        public Entity(string modelName, Transform transform, Color color)
        {
            ModelName = modelName;
            Transform = transform;
            Color = color;
            if (ModelName == "Cube")
            {
                Model = new Cube(Color);
            }
            else
            {
                Model = new(Color);
            }
            Model.ApplyTransform(Transform);
        }

        /// <summary>
        /// Check if a given <c>Entity</c> is inside the clipping volume defined by
        /// a list of <c>Plane</c>.
        /// </summary>
        /// <param name="planes">A list of planes that define the clipping volume.</param>
        /// <returns>Returns <c>true</c> if the <c>Entity</c> is completely inside the
        /// clipping volume and <c>false</c> otherwise.</returns>
        public static bool IsInsideClippingVolume(Entity entity, List<Plane> planes)
        {
            bool isInside = true;
            int i = 0;
            while (i < planes.Count && isInside)
            {
                float signedDistance = planes[i].ComputeSignedDistance(entity.Model.BoundingCenter);
                if (signedDistance <= entity.Model.BoundingRadius)
                {
                    isInside = false;
                }
                i++;
            }
            return isInside;
        }

        /// <summary>
        /// Clip this <c>Entity</c> against a list of <c>Plane</c>.
        /// </summary>
        /// <param name="planes">A list of clipping planes.</param>
        /// <returns>The clipped <c>Entity</c>, or <c>null</c> is the <c>Entity</c> is outside
        /// a clipping volume.</returns>
        public Entity? ClipAgainstPlanes(List<Plane> planes)
        {
            return ClipEntityAgainstPlanes(this, planes);
        }

        /// <summary>
        /// Clip a given <c>Entity</c> against a list of <c>Plane</c>.
        /// </summary>
        /// <param name="entity">The <c>Entity</c> to clip.</param>
        /// <param name="planes">A list of clipping planes.</param>
        /// <returns>The clipped <c>Entity</c>, or <c>null</c> is the <c>Entity</c> is outside
        /// a clipping volume.</returns>
        public static Entity? ClipEntityAgainstPlanes(Entity entity, List<Plane> planes)
        {
            Entity? clippedEntity = Entity.Clone(entity);
            foreach (Plane plane in planes)
            {
                clippedEntity = ClipEntityAgainstPlane(clippedEntity, plane);
                if (clippedEntity == null)
                {
                    return null;
                }
            }
            return clippedEntity;
        }

        /// <summary>
        /// Clip a given <c>Entity</c> against a <c>Plane</c>.
        /// </summary>
        /// <param name="entity">The <c>Entity</c> to clip.</param>
        /// <param name="plane">The clipping plane.</param>
        /// <returns>Return the original <c>Entity</c> if it's inside the clipping volume, <c>null</c>
        /// if it's outside the clipping volume or a clipped <c>Entity</c> if the original <c>Entity</c>
        /// overlaps the clipping plane.</returns>
        public static Entity? ClipEntityAgainstPlane(Entity entity, Plane plane)
        {
            float signedDistance = plane.ComputeSignedDistance(entity.Model.BoundingCenter);
            if (signedDistance > entity.Model.BoundingRadius)
            {
                // This entity is completely inside the clipping volume. Return the
                // original entity.
                return entity;
            }
            else if (signedDistance < -entity.Model.BoundingRadius)
            {
                // This entity is completely outside the clipping volume. Return null.
                return null;
            }
            else
            {
                // This entity overlaps the clipping plane. Return the clipped entity.
                Entity clippedEntity = new("Generic", entity.Transform, entity.Color);
                clippedEntity.Model.SetTriangles(Triangle.ClipTrianglesAgainstPlane(entity.Model.Triangles, plane));
                return clippedEntity;
            }
        }

    }
}
