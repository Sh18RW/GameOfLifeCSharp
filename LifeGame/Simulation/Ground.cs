namespace LifeGame.Simulation
{
    public class Ground
    {
        // world with extended world size (not fixed)
        public static Ground LoadGround()
        {
            return null;
        }
        // world with const size
        public static Ground LoadGround(int worldSize)
        {
            return null;
        }
        // loading from serialization
        public static Ground LoadGround(string loadFrom)
        {
            return null;
        }

        private enum GroundSizeType
        {
            Const,
            Extended
        }
    }
}