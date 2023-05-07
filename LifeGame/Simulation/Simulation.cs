namespace Simulation
{
    public class Simulation
    {
        private ECell[][] _field;
        private bool _gameState_isStarted;

        public Simulation(int fieldSize)
        {
            _field = new ECell[fieldSize][];
            
            Init(fieldSize);
        }

        public void Play()
        {
            int step = 0;

            while (true)
            {
                if (!_gameState_isStarted)
                {
                    _gameState_isStarted = SetupState.Update(ref _field);
                    Console.Clear();
                    // Graphics.DrawField(_field);
                }
                else
                {
                    Update();

                    Console.Clear();
                    // Graphics.DrawField(_field);
                    Console.WriteLine(step++);

                    Thread.Sleep(2000);
                }
            }
        }

        private void Init(int fieldSize)
        {
            InitField(fieldSize);

            _gameState_isStarted = false;
        }

        private void InitField(int fieldSize)
        {
            for (int i = 0;i < fieldSize;i++)
            {
                _field[i] = new ECell[fieldSize];

                for (int j = 0;j < fieldSize;j++)
                {
                    _field[i][j] = ECell.Empty;
                }
            }
        }

        private void Update()
        {
            int[][] neighborsField = new int[_field.Length][];

            MathNeighborsField(ref neighborsField);
            UpdateField(ref neighborsField);
        }

        private void MathNeighborsField(ref int[][] neighborsField)
        {

            for (int i = 0;i < _field.Length;i++)
            {
                neighborsField[i] = new int[_field.Length];

                for (int j = 0;j < _field.Length;j++)
                {
                    for (int k = 0;k < 9;k++)
                    {
                        int x = k % 3 - 1;
                        int y = k / 3 - 1;

                        if (x == 0 && y == 0) continue;

                        neighborsField[i][j] += GetNeighbor(x + i, y + j);
                    }
                }
            }
        }

        private void UpdateField(ref int[][] neighborsField)
        {
            for (int y = 0;y < _field.Length;y++)
            {
                for (int x = 0;x < _field.Length;x++)
                {
                    if (_field[x][y] == ECell.Alive)
                    {
                        if (neighborsField[x][y] < 2 || neighborsField[x][y] > 3)
                        {
                            _field[x][y] = ECell.Empty;
                        }
                    }
                    else
                    {
                        if (neighborsField[x][y] == 3)
                        {
                            _field[x][y] = ECell.Alive;
                        }
                    }
                }
            }
        }

        private int GetNeighbor(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _field.Length || y >= _field.Length)
            {
                return 0;
            }

            return _field[x][y] == ECell.Alive ? 1 : 0;
        }
    }
}