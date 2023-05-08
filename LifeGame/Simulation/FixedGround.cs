using LifeGame.Simulation.Logic;

namespace LifeGame.Simulation.Ground
{
    public class FixedGround : Ground
    {
        private readonly int _groundSize;

        public FixedGround(int groundSize)
        {
            _groundSize = groundSize;

            _tilemap.Add(0, new Tile(_groundSize));
        }

        public int GetGroundSize()
        {
            return _groundSize;
        }

        public override ECell? GetCell(int x, int y)
        {
            if (!CheckCellAvailable(x, y)) return null;

            return _tilemap[0].GetCell(x, y);
        }

        public override void SetCell(int x, int y)
        {
            if (CheckCellAvailable(x, y))
            {
                _tilemap[0].SetCell(x, y);
            }
        }

        public override void Update()
        {
            int[][] tile = new int[_groundSize][];

            for (int y = 0;y < _groundSize;y++)
            {
                tile[y] = new int[_groundSize];
                
                for (int x = 0;x < _groundSize;x++)
                {
                    for (int i = 0;i < 9;i++)
                    {
                        int xOffset = (i % 3) - 1;
                        int yOffset = (i / 3) - 1;

                        if (xOffset != 0 || yOffset != 0)
                        {
                            int finalX = x + xOffset;
                            int finalY = y + yOffset;

                            var cell = _tilemap[0].GetCell(finalX, finalY);

                            if (cell != null)
                                tile[y][x] += cell == ECell.Alive ? 1 : 0;
                        }
                    }
                }
            }

            for (int y = 0;y < _groundSize;y++)
            {
                for (int x = 0;x < _groundSize;x++)
                {
                    if (tile[y][x] > 3 || tile[y][x] < 2) _tilemap[0].SetCell(x, y, ECell.Empty);
                    else if (tile[y][x] == 3) _tilemap[0].SetCell(x, y, ECell.Alive);
                }
            }
        }

        private bool CheckCellAvailable(int x, int y)
        {
            return !(x < 0 || y < 0 || x >= _groundSize || y >= _groundSize);
        }
    }
}