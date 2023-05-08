namespace LifeGame.Simulation.Logic
{
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
                Console.WriteLine($"Null at {x} {y}");
                return null;
            }
            return _tile[y][x];
        }

        public int GetCountOfAliveCells()
        {
            return _countOfAliveCells;
        }
    }
}