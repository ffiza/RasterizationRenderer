namespace RasterizationRenderer.Utils
{
    public class Scene
    {
        public List<Entity> Entities { get; private set; } = new();
        private readonly Viewport _viewport;

        public Scene(Viewport viewport)
        {
            _viewport = viewport;
        }

        /// <summary>
        /// Add a new entity to this scene.
        /// </summary>
        /// <param name="entity">The new entity.</param>
        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Render this scene into the given canvas.
        /// </summary>
        /// <param name="canvas">The canvas onto which to render the scene.</param>
        public void Render(Canvas canvas)
        {
            foreach (Entity entity in Entities)
            {
                foreach (Triangle triangle in entity.Triangles)
                {
                    canvas.DrawWireframeTriangle(Coordinates.WorldToCanvas(triangle.Vertices[0], _viewport, canvas),
                                                 Coordinates.WorldToCanvas(triangle.Vertices[1], _viewport, canvas),
                                                 Coordinates.WorldToCanvas(triangle.Vertices[2], _viewport, canvas),
                                                 triangle.Color);
                }
            }
        }
    }
}
