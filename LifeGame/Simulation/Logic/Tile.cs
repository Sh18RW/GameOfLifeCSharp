namespace LifeGame.Simulation.Logic
{
    public class Tile
    {
        private readonly int _size;

        private ECell[][] tile;

        public Tile(int size)
        {
            _size = size;

            tile = new ECell[_size][];

            for (int i = 0;i < _size;i++)
            {
                tile[i] = new ECell[_size];

                for (int j = 0;j < _size;j++)
                {
                    tile[i][j] = ECell.Empty;
                }
            }
        }

        public void SetCell(int x, int y)
        {
            SetCell(x, y, tile[y][x] == ECell.Empty ? ECell.Alive : ECell.Empty);
        }

        public void SetCell(int x, int y, ECell state)
        {
            tile[y][x] = state;
        }

        public ECell? GetCell(int x, int y)
        {
            if (x >= _size || x < 0 || y >= _size || y < 0) return null;
            return tile[y][x];
        }
    }
}