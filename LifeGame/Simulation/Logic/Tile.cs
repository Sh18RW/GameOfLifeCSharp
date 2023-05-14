using System.Text;
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

        public XmlNode MakeXmlNode(XmlDocument document)
        {
            var root = document.CreateElement("tileCells");

            foreach (var line in _tile)
            {
                var xmlLine = document.CreateElement("line");

                var lineString = new StringBuilder();

                foreach (var cell in line)
                {
                    lineString.Append((int) cell);
                }

                var lineText = document.CreateTextNode(lineString.ToString());

                xmlLine.AppendChild(lineText);

                root.AppendChild(xmlLine);
            }

            return root;
        }


        public static Tile MakeTileFromXml(XmlElement element, int tileSize)
        {
            var lines = element.GetElementsByTagName("line");

            var tile = new Tile(tileSize);

            for (int y = 0;y < tileSize;y++)
            {
                var line = GetCellsLine(lines, y);

                for (int x = 0;x < tileSize;x++)
                {
                    var cell = line[x];

                    tile.SetCell(x, y, (ECell) int.Parse(cell.ToString()));
                }
            }

            return tile;
        }

        private static string GetCellsLine(XmlNodeList list, int i)
        {
            try
            {
                if (list.Count < i) throw new Exception();

                var cellsLine = (list[i] ?? throw new Exception()).FirstChild as XmlText ?? throw new Exception();

                return cellsLine.Value ?? throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("Failed on tile parsing");
            }
        }
    }
}