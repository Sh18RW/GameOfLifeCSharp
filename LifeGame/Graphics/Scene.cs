namespace LifeGame.Graphics
{
    public abstract class Scene
    {
        private protected readonly IContext _context;

        public Scene(IContext context)
        {
            _context = context;
        }

        public void Update()
        {
            var text = Draw();
            Console.Clear();
            Console.WriteLine(text);
            UpdateControl();
        }
        private protected abstract string Draw();
        private protected abstract void UpdateControl();
    }
}