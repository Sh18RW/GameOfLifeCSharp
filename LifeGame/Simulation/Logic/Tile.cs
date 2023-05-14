using System.Xml;

namespace LifeGame.Simulation.Logic
{
    [Serializable]
    public class Tile
    {
        private readonly int _size;

        private readonly ECell[][] _tile;
        private int _countOfAliveCells;

        public Tile(int size)
        {
            _size = size;
            _countOfAliveCells = 0;

            _tile = new ECell[_size][];

            for (int i = 0;i < _size;i++)
            {
                _tile[i] = new ECell[_size];

                for (int j = 0;j < _size;j++)
                {
                    _tile[i][j] = ECell.Empty;
                }
            }
        }

        public void SetCell(int x, int y)
        {
            SetCell(x, y, _tile[y][x] == ECell.Empty ? ECell.Alive : ECell.Empty);
        }

        public void SetCell(int x, int y, ECell state)
        {
            if (_tile[y][x] != ECell.Alive && state == ECell.Alive)
            {
                _countOfAliveCells++;
            }
            else if (_tile[y][x] != ECell.Empty && state == ECell.Empty)
            {
                _countOfAliveCells--;
            }

            _tile[y][x] = state;
        }

        public ECell? GetCell(int x, int y)
        {
            if (x >= _size || x < 0 || y >= _size || y < 0) {
                return null;
            }
            return _tile[y][x];
        }

        public int GetCountOfAliveCells()
        {
            return _countOfAliveCells;
        }

        public XmlNode MakeXmlNode(XmlDocument root)
        {
            var tileValuesRoot = root.CreateElement("value");

            foreach (var line in _tile)
            {
                var xmlLine = root.CreateElement("line");

                foreach (var i in line)
                {
                    var elementXml = root.CreateElement("cell");

                    elementXml.AppendChild(root.CreateTextNode(i.ToString()));

                    xmlLine.AppendChild(elementXml);
                }

                tileValuesRoot.AppendChild(xmlLine);
            }

            return tileValuesRoot;
        }

        
        public static Tile MakeTileFromXml(XmlElement element, int tileSize)
        {
            var tile = new Tile(tileSize);

            for (var y = 0;y < tileSize;y++)
            {
                var line = element.ChildNodes[y];
                for (var x = 0;x < tileSize;x++)
                {
                    var cellNode = (line as XmlElement).ChildNodes[x];
                    ECell cell = (ECell) int.Parse(((XmlText) cellNode).ToString());
                    tile.SetCell(x, y, cell);
                }
            }

            return tile;
        }
    }
}