using Newtonsoft.Json;

namespace RasterizationRenderer.Utils
{
    public class Scene
    {
        public string Name { get; private set; }
        public Viewport Viewport { get; private set; }
        public List<Entity> Entities { get; private set; }

        public Scene(string name, Viewport viewport, List<Entity> entities)
        {
            Name = name;
            Viewport = viewport;
            Entities = entities;
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
            foreach (Entity entity in _entities)
            {
                foreach (Triangle triangle in entity.Model.Triangles)
                {
                    canvas.DrawWireframeTriangle(Coordinates.WorldToCanvas(triangle.Vertices[0], _viewport, canvas),
                                                 Coordinates.WorldToCanvas(triangle.Vertices[1], _viewport, canvas),
                                                 Coordinates.WorldToCanvas(triangle.Vertices[2], _viewport, canvas),
                                                 triangle.Color);
                }
            }
        }

        /// <summary>
        /// Read a JSON file with scene data and return a new instance of <c>Scene</c>.
        /// </summary>
        /// <param name="path">The path to the JSON file.</param>
        /// <returns></returns>
        /// <exception cref="Exception">If the Scene is `null`, raise exception.</exception>
        public static Scene FromJSON(string path)
        {
            Scene? scene;
            using (StreamReader r = new(path))
            {
                string json = r.ReadToEnd();
                scene = JsonConvert.DeserializeObject<Scene>(json);
            }
            if (scene == null)
            {
                throw new Exception("Null scene loaded.");
            }
            return scene;
        }
    }
}
