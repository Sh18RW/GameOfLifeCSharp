using LifeGame.Simulation.Ground;

namespace LifeGame.Graphics.Scenes
{
    public class Scene_Simulation : Scene
    {
        // private Simulation _simlulation;

        public Scene_Simulation(IContext context, Ground ground) : base(context)
        {
            Console.WriteLine("Simulation Scene constructor");
        }

        private protected override string Draw()
        {
            throw new NotImplementedException();
        }

        private protected override void UpdateControl()
        {
            throw new NotImplementedException();
        }
    }
}