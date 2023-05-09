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
            // TODO: make loading for ground 
            return null;
        }

        public void SaveGround(string saveFrom)
        {
            // TODO: make saving for ground

            try
            {
                var document = new XmlDocument();
                MakeSaveFile(document);

                document.Save(saveFrom);
            } catch(Exception e)
            {

                Console.WriteLine($"Some problems while saving: {e.Message}");
                Console.ReadKey();
            }
        }

        public abstract void Update();
        public abstract void SetCell(int x, int y);
        public abstract ECell? GetCell(int x, int y);
        public abstract int GetTileSize();

        private protected virtual void MakeSaveFile(XmlDocument document)
        {
            var root = document.CreateElement("ground");
            document.AppendChild(newChild: root);
            var tilesRoot = document.CreateElement("tiles");

            var tileSizeInfo = document.CreateElement("tileSize");

            tileSizeInfo.AppendChild(document.CreateTextNode(GetTileSize().ToString()));

            foreach (var key in _tilemap.Keys)
            {
                var element = _tilemap[key];

                var xmlTileInfo = document.CreateElement("tileInfo");

                xmlTileInfo.AppendChild(key.MakeXmlNode(document));
                xmlTileInfo.AppendChild(element.MakeXmlNode(document));

                tilesRoot.AppendChild(xmlTileInfo);
            }

            root.AppendChild(tileSizeInfo);
            root.AppendChild(tilesRoot);
        }

        private protected virtual Ground GetGroundFromXml(XmlDocument document)
        {
            var root = document.FirstChild as XmlElement;
            string type;

            foreach (XmlElement element in root.ChildNodes)
            {
                if (element.Name.Equals("groundType"))
                {
                    type = (element.FirstChild as XmlText).ToString();
                }
                else
                {
                    foreach (XmlElement tile in element.ChildNodes)
                    {
                        Point point;
                        Tile currentTile;
                        foreach (XmlElement line in tile.ChildNodes)
                        {
                            if (line.Name.Equals("point"))
                            {
                                point = Point.MakePointFromXml(line);
                            }
                            else
                            {
                                // currentTile = Tile.MakeTileFromXml(line, tileSize);
                            }
                        }
                    }
                }
            }

            return null;
        }
    }

}