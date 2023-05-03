namespace ConsoleGraphics
{
    public class Scene
    {
        private List<Component> _components;

        public Scene()
        {
            _components = new List<Component>();
        }

        public void AddComponent(Component newComponent)
        {
            _components.Add(newComponent);
        }

        internal void Draw()
        {
            foreach (var component in _components)
            {
                Console.WriteLine(component.Draw());
            }
        }
    }
}