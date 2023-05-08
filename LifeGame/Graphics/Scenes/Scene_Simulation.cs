using LifeGame.Simulation.Ground;
using LifeGame.Simulation.Logic;
using System.Text;

namespace LifeGame.Graphics.Scenes
{
    public class SceneSimulation : Scene
    {
        private readonly Ground _ground;

        private int _screenWidth, _screenHeight;
        private int _xCenter, _yCenter;
        private int _xOffset, _yOffset;

        private bool _isPlaying;
        private int _delay;

        private Thread? _updatingThread;

        public SceneSimulation(IContext context, Ground ground) : base(context)
        {
            _ground = ground;

            UpdateVideoParametrs();

            _isPlaying = false;
            _delay = 1000;
        }

        private protected override string Draw()
        {
            var result = new StringBuilder();

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

            _ = result.Append($"Press 'm' to stop simulation and enter the command\t\t|{_isPlaying}|{_delay}ms|");

            return result.ToString();
        }

        private protected override void UpdateControl()
        {
            if (_isPlaying && !Console.KeyAvailable)
            {
                Thread.Sleep(100);
                return;
            }

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.M:
                    _isPlaying = false;

                    Console.WriteLine("Write 'help' to get a list of commands");

                    while(true)
                    {
                        string[] command = (Console.ReadLine() ?? "").Split(' ');

                        if (command.Length > 0)
                        {
                            switch (command[0])
                            {
                                case "help":
                                    break;
                                case "update_screen":
                                    UpdateVideoParametrs();
                                    break;
                                case "continue_editing":
                                    return;
                                case "set_delay":
                                    if (command.Length < 2)
                                    {
                                        Console.WriteLine("Can't find first argument");
                                    }
                                    if (!int.TryParse(command[1], out _delay))
                                    {
                                        if (_delay <= 0)
                                        {
                                            _delay = 100;
                                            Console.WriteLine("Wrong value for delay, seting up to 100");
                                        }
                                        else if (_delay > 4000)
                                        {
                                            _delay = 4000;
                                            Console.WriteLine("Wrong value for delay, setting down to 4000");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Something wrong in integer parsing");
                                    }
                                    Console.WriteLine($"Now delay is {_delay}ms");
                                    break;
                                case "play":
                                    _isPlaying = true;
                                    new Thread(() => {
                                        while (_isPlaying)
                                        {
                                            _ground.Update();
                                            Thread.Sleep(_delay);
                                        }
                                    }).Start();
                                    return;
                            }
                        }
                    }
                case ConsoleKey.S:
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
                    int x = (_xCenter + _xOffset - 1) / 2;
                    int y = (_yCenter + _yOffset - 1) / 2;

                    _ground.SetCell(x, y);
                    break;
            }
        }

        private void UpdateVideoParametrs()
        {
            _screenWidth = Console.WindowWidth;
            _screenHeight = Console.WindowHeight - 1;

            _xCenter = (_screenWidth / 2) + (_screenWidth % 2);
            _yCenter = (_screenHeight / 2) + (_screenHeight % 2);

            // _xOffset = -(_screenWidth / 2);
            // _yOffset = -(_screenHeight / 2);

            _xOffset = -(_screenWidth / 2) + 1 - (_screenWidth % 2);
            _yOffset = -(_screenHeight / 2) + 1 - (_screenHeight % 2);
        }
    }
}