using System.Xml;
using LifeGame.Simulation.Logic;

namespace LifeGame.Simulation.Ground
{
    public abstract class Ground
    {
        private protected readonly Dictionary<Point, Tile> _tilemap;
        private protected Ground()
        {
            _tilemap = new Dictionary<Point, Tile>();
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
            var document = new XmlDocument();
            document.Load(loadFrom);
            return GetGroundFromXml(document);
        }

        public void SaveGround(string saveFrom)
        {
            // TODO: make saving for ground

            try
            {
                var document = new XmlDocument();

                document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", null));

                MakeSaveFile(document);

                document.Save(saveFrom);
            }
            catch(Exception e)
            {

                Console.WriteLine($"Some problems while saving: {e.Message}");
                Console.ReadKey();
            }
        }

        public abstract void Update();
        public abstract void SetCell(int x, int y);
        public abstract ECell? GetCell(int x, int y);
        public abstract int GetTileSize();
        private protected abstract string GetGroundType();

        private protected void MakeSaveFile(XmlDocument document)
        {
            var root = document.CreateElement("ground");

            var groundTypeInfo = document.CreateElement("groundType");
            var groundTileInfo = document.CreateElement("tileSize");

            groundTypeInfo.AppendChild(document.CreateTextNode(GetGroundType()));
            groundTileInfo.AppendChild(document.CreateTextNode(GetTileSize().ToString()));

            root.AppendChild(groundTypeInfo);
            root.AppendChild(groundTileInfo);

            var tilesElement = document.CreateElement("tiles");

            foreach (var key in _tilemap.Keys)
            {
                var tile = _tilemap[key];

                var tileElement = document.CreateElement("tile");

                var pointElement = key.MakeXmlNode(document);
                var tileInfoElement = tile.MakeXmlNode(document);

                tileElement.AppendChild(pointElement);
                tileElement.AppendChild(tileInfoElement);

                tilesElement.AppendChild(tileElement);
            }

            root.AppendChild(tilesElement);

            document.AppendChild(root);
        }

        private protected static Ground GetGroundFromXml(XmlDocument document)
        {
            var groundType = GetStringValue(document, "groundType");
            if(!int.TryParse(GetStringValue(document, "tileSize"), out var tileSize)) throw new Exception("Can't parse tile size");

            var tilesElement = document.GetElementsByTagName("tile");

            Ground ground = groundType.Equals("fixes") ? new FixedGround(tileSize) : new ExtendedGround();

            foreach (XmlElement tileElement in tilesElement)
            {
                var pointElement = tileElement.GetElementsByTagName("point")[0] ?? throw new Exception("Can't read point");
                var tileInfoElement = tileElement.GetElementsByTagName("tileCells")[0] ?? throw new Exception("Can't read tile");

                var point = Point.MakePointFromXml(pointElement as XmlElement ?? throw new Exception("Can't parse point"));
                var tile = Tile.MakeTileFromXml(tileInfoElement as XmlElement ?? throw new Exception("Can't parse tile"), tileSize);

                ground._tilemap.Remove(point);

                ground._tilemap[point] = tile;
            }

            return ground;
        }

        private static string GetStringValue(XmlDocument root, string tag)
        {
            try
            {
                var list = root.GetElementsByTagName(tag);

                if (list.Count == 0) throw new Exception();

                var result = (list[0] ?? throw new Exception()).FirstChild ?? throw new Exception();

                return result.Value ?? throw new Exception();
            }
            catch(Exception)
            {
                throw new Exception("Failed on ground info parsing");
            }
        }
    }

}