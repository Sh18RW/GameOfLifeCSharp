namespace LifeGame.Simulation.Ground
{
    public class FixedGround : Ground
    {
        private readonly int _groundSize;

        public FixedGround(int groundSize)
        {
            _groundSize = groundSize;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}