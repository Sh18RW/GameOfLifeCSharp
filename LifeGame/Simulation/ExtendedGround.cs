using LifeGame.Simulation.Logic;

namespace LifeGame.Simulation.Ground
{
    public class ExtendedGround : Ground
    {
        private static readonly int tileSize = 10;
        public ExtendedGround()
        {
        }

        public override ECell? GetCell(int x, int y)
        {
            int tileX = (x + (x < 0 ? 1 : 0)) / tileSize - (x < 0 ? 1 : 0);
            int tileY = (y + (y < 0 ? 1 : 0)) / tileSize - (y < 0 ? 1 : 0);

            if (!_tilemap.ContainsKey((tileX, tileY)))
            {
                return ECell.Empty;
            }

            return _tilemap[(tileX, tileY)].GetCell(x - tileX * tileSize, y - tileY * tileSize);
        }

        public override void SetCell(int x, int y)
        {
            int tileX = x / tileSize - (x < 0 ? 1 : 0);
            int tileY = y / tileSize - (y < 0 ? 1 : 0);
            
            if (!_tilemap.ContainsKey((tileX, tileY)))
            {
                _tilemap.Add((tileX, tileY), new Tile(tileSize));
            }

            _tilemap[(tileX, tileY)].SetCell(x - tileX * tileSize, y - tileY * tileSize);
        }

        public static int GetTileSize()
        {
            return tileSize;
        }

        public override void Update()
        {
            var tileIndexesDictionary = new Dictionary<Point, int[][]>();
            foreach (var tilePoint in _tilemap.Keys)
            {
                var tile = _tilemap[tilePoint];
                bool isIntiailized = tileIndexesDictionary.ContainsKey(tilePoint);

                int[][] tileIndexes;

                if (isIntiailized)
                {
                    tileIndexes = tileIndexesDictionary[tilePoint];
                }
                else
                {
                    tileIndexes = new int[tileSize][];
                }

                for (int y = 0;y < tileSize;y++)
                {
                    if(!isIntiailized)
                        tileIndexes[y] = new int[tileSize];

                    for (int x = 0;x < tileSize;x++)
                    {
                        if (tile.GetCell(x, y) == ECell.Alive){
                            for (int i = 0;i < 9;i++)
                            {
                                int localX = i % 3 - 1;
                                int localY = i / 3 - 1;

                                if (localX != 0 || localY != 0)
                                {
                                    int finalX = x + localX;
                                    int finalY = y + localY;

                                    int tileFinalX = tilePoint.X + (finalX < 0 ? -1 : finalX >= tileSize ? 1 : 0);
                                    int tileFinalY = tilePoint.Y + (finalY < 0 ? -1 : finalY >= tileSize ? 1 : 0);

                                    bool hasThisTile = tileIndexesDictionary.ContainsKey((tileFinalX, tileFinalY));
                                    int[][] currentTileIndexes;

                                    if (!hasThisTile)
                                    {
                                        currentTileIndexes = new int[tileSize][];

                                        for (int j = 0;j < tileSize;j++)
                                        {
                                            currentTileIndexes[j] = new int[tileSize];
                                        }
                                    }
                                    else
                                    {
                                        currentTileIndexes = tileIndexesDictionary[(tileFinalX, tileFinalY)];
                                    }

                                    finalX = finalX < 0 ? tileSize + finalX : finalX >= tileSize ? finalX - tileSize : finalX;
                                    finalY = finalY < 0 ? tileSize + finalY : finalY >= tileSize ? finalY - tileSize : finalY;

                                    currentTileIndexes[finalY][finalX]++;

                                    if (!hasThisTile)
                                        tileIndexesDictionary.Add((tileFinalX, tileFinalY), currentTileIndexes);
                                }
                            }
                        }
                    }
                }

                // I don't know for what it
                // if (!isIntiailized)
                //     tileIndexesDictionary.Add(tilePoint, tileIndexes);
            }

            foreach (var key in tileIndexesDictionary.Keys)
            {
                int[][] tileIndexes = tileIndexesDictionary[key];

                Tile currentTile;
                bool isIntiailized = _tilemap.ContainsKey(key);

                if (isIntiailized)
                {
                    currentTile = _tilemap[key];
                }
                else
                {
                    currentTile = new Tile(tileSize);
                }

                for (int x = 0;x < tileSize;x++)
                {
                    for (int y = 0;y < tileSize;y++)
                    {
                        int countOfAliveCells = tileIndexes[y][x];

                        if (countOfAliveCells < 2 || countOfAliveCells > 3)
                        {
                            currentTile.SetCell(x, y, ECell.Empty);
                        }
                        else if (countOfAliveCells == 3)
                        {
                            currentTile.SetCell(x, y, ECell.Alive);
                        }
                    }
                }

                if (currentTile.GetCountOfAliveCells() > 0 && !isIntiailized)
                {
                    _tilemap.Add(key, currentTile);
                }
                else if (currentTile.GetCountOfAliveCells() <= 0 && isIntiailized)
                {
                    _tilemap.Remove(key);
                }
            }
        }
    }
}