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
            Console.Clear();
            Console.WriteLine(Draw());
            UpdateControl();
        }
        private protected abstract string Draw();
        private protected abstract void UpdateControl();
    }
}