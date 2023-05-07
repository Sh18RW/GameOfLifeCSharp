namespace LifeGame.Graphics
{
    public abstract class Scene
    {
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