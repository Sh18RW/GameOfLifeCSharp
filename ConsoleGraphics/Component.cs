namespace ConsoleGraphics
{
    public abstract class Component
    {
        private protected List<Component> _childrens;

        public virtual void AddComponent(Component component)
        {
            _childrens.Add(component);
        }
        private protected Component()
        {
            _childrens = new List<Component>();
        }
        internal abstract string Draw();
    }
}