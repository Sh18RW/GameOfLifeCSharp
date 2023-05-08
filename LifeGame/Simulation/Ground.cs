using System.Xml.Serialization;
using LifeGame.Simulation.Logic;

namespace LifeGame.Simulation.Ground
{
    [Serializable]
    public abstract class Ground
    {
        private protected readonly Dictionary<Point, Logic.Tile> _tilemap;
        private protected Ground()
        {
            _tilemap = new Dictionary<Point, Logic.Tile>();
        }

        // world with extended world size (not fixed)
        public static Ground LoadGround()
        {
            return new ExtendedGround();
        }
        // world with const size
        public static Ground LoadGround(int worldSize)
        {
            return new FixedGround(worldSize);
        }
        // loading from serialization
        public static Ground? LoadGround(string loadFrom)
        {
            var serializer = new XmlSerializer(typeof(Ground));
            try
            {
                var fs = new FileStream(loadFrom, FileMode.Open);
                return serializer.Deserialize(fs) as Ground;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Can't find file");
            }

            return null;
        }

        public abstract void Update();
        public abstract void SetCell(int x, int y);
        public abstract Logic.ECell? GetCell(int x, int y);
    }
}