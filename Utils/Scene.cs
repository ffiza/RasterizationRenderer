using Newtonsoft.Json;
using System.Numerics;
using System.Drawing;

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
            // Render the clipped scene
            Scene clippedScene = Clip(Viewport.ClippingPlanes);
            foreach (Entity entity in clippedScene.Entities)
            {
                foreach (Triangle triangle in entity.Model.Triangles)
                {
                    if (triangle.IsFrontFacing(triangle.Vertices[0] - Viewport.CameraPosition))
                    {
                        Vector2 p0 = Coordinates.WorldToCanvas(triangle.Vertices[0], Viewport, canvas);
                        Vector2 p1 = Coordinates.WorldToCanvas(triangle.Vertices[1], Viewport, canvas);
                        Vector2 p2 = Coordinates.WorldToCanvas(triangle.Vertices[2], Viewport, canvas);
                        float z0 = triangle.Vertices[0].Z;
                        float z1 = triangle.Vertices[1].Z;
                        float z2 = triangle.Vertices[2].Z;
                        canvas.DrawFilledTriangle(p0, p1, p2, z0, z1, z2, triangle.Color); 
                    }
                }
            }
        }

        /// <summary>
        /// Clip this <c>Scene</c> against a list of <c>Plane</c>.
        /// </summary>
        /// <param name="planes">A list of clipping planes.</param>
        /// <returns>The clipped scene.</returns>
        public Scene Clip(List<Plane> planes)
        {
            return ClipScene(this, planes);
        }

        /// <summary>
        /// Clip a given <c>Scene</c> against a list of <c>Plane</c>.
        /// </summary>
        /// <param name="scene">The <c>Scene</c> instance to clip.</param>
        /// <param name="planes">A list of clipping planes.</param>
        /// <returns>The clipped scene.</returns>
        public static Scene ClipScene(Scene scene, List<Plane> planes)
        {
            Scene clippedScene = new(scene.Name, scene.Viewport, new());
            foreach (Entity entity in scene.Entities)
            {
                Entity? clippedEntity = entity.ClipAgainstPlanes(planes);
                if (clippedEntity != null)
                {
                    clippedScene.Entities.Add(clippedEntity);
                }
            }
            return clippedScene;
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
