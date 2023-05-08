using LifeGame.Simulation.Ground;
using LifeGame.Simulation.Logic;
using System.Text;

namespace LifeGame.Graphics.Scenes
{
    public class SceneSimulation : Scene
    {
        private readonly Ground _ground;

        private readonly int _screenWidth, _screenHeight;
        private int _xOffset, _yOffset;
        private readonly int _xCenter, _yCenter;

        public SceneSimulation(IContext context, Ground ground) : base(context)
        {
            _ground = ground;

            _screenWidth = Console.WindowWidth;
            _screenHeight = Console.WindowHeight - 1;

            _xCenter = (_screenWidth / 2) + (_screenWidth % 2);
            _yCenter = (_screenHeight / 2) + (_screenHeight % 2);

            // _xOffset = -(_screenWidth / 2);
            // _yOffset = -(_screenHeight / 2);

            _xOffset = -(_screenWidth / 2) + 1 - (_screenWidth % 2);
            _yOffset = -(_screenHeight / 2) + 1 - (_screenHeight % 2);
        }

        private protected override string Draw()
        {
            StringBuilder result = new StringBuilder();

            for (int y = 0; y < _screenHeight - 1; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    if (y == 0 || y == _screenHeight - 2)
                    {
                        _ = result.Append('-');
                    }
                    else if (x == 0 || x == _screenWidth - 1)
                    {
                        _ = result.Append('|');
                    }
                    else
                    {
                        int bareX = x + _xOffset - 1;
                        int bareY = y + _yOffset - 1;

                        if (bareX % 2 == 1 || bareX % 2 == -1 || bareY % 2 == 1 || bareY % 2 == -1)
                        {
                            if (y == _yCenter)
                            {
                                if (x == _xCenter - 1)
                                {
                                    _ = result.Append('>');
                                    continue;
                                }
                                else if (x == _xCenter + 1)
                                {
                                    _ = result.Append('<');
                                    continue;
                                }
                            }

                            _ = result.Append(' ');
                        }
                        else
                        {
                            ECell? cell = _ground.GetCell(bareX / 2, bareY / 2);

                            if (cell == null)
                            {
                                _ = result.Append('~');
                            }
                            else
                            {
                                if (cell == ECell.Alive)
                                {
                                    _ = result.Append('X');
                                }
                                else
                                {
                                    _ = result.Append('\'');
                                }
                            }
                        }
                    }
                }

                _ = result.Append('\n');
            }

            _ = result.Append($"Press 's' to stop simulation and enter the command {_xOffset / 2} {_yOffset / 2}");

            return result.ToString();
        }

        private protected override void UpdateControl()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.S:
                    Console.WriteLine("You paused simulation");
                    _ = Console.ReadKey();
                    _ground.Update();
                    break;
                case ConsoleKey.UpArrow:
                    _yOffset -= 2;
                    break;
                case ConsoleKey.DownArrow:
                    _yOffset += 2;
                    break;
                case ConsoleKey.LeftArrow:
                    _xOffset -= 2;
                    break;
                case ConsoleKey.RightArrow:
                    _xOffset += 2;
                    break;
                case ConsoleKey.Enter:
                    int x = (_xCenter + _xOffset) / 2;
                    int y = (_yCenter + _yOffset) / 2;

                    _ground.SetCell(x, y);
                    break;
            }

        }
    }
}