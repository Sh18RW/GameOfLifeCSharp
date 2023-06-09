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

        private bool _isJustSaved;

        private Thread? _currentUpdatingThread;

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

            _ = result.Append($"Press 'c' to stop simulation and enter the command\t|{_isPlaying}|{_delay}ms| |{(_xCenter + _xOffset) / 2}||{(_yCenter + _yOffset) / 2}|");

            return result.ToString();
        }

        private protected override void UpdateControl()
        {
            if (_isPlaying && !Console.KeyAvailable)
            {
                Thread.Sleep(100);
                return;
            }
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.C:
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
                                    Console.WriteLine("update_screen - update screen paramets");
                                    Console.WriteLine("continue_editing - exit console to continue editing");
                                    Console.WriteLine("set_delay - set delay between simulation updating (don't set it lower then 100 and higher than 4000)");
                                    Console.WriteLine("play - start the simulation");
                                    Console.WriteLine("save - open save screen");
                                    Console.WriteLine("quit - exit simulation screen");
                                    Console.WriteLine("Press 'C' to open console;Press 'N' to do step in simulation;Press 'P' to start playing simulation;Press 'S' to stop playing simulation");
                                    Console.WriteLine("For moving use arrows on keyboard");
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
                                    PlaySimulation();

                                    return;
                                case "save":
                                    SaveCurrentGround();
                                    break;
                                case "quit":
                                    if (!_isJustSaved)
                                    {
                                        Console.WriteLine("You didn't save the current simulation state. Do you want to save? [Y/n]");

                                        while (true)
                                        {
                                            switch (Console.ReadKey(true).Key)
                                            {
                                                case ConsoleKey.Y:
                                                    SaveCurrentGround();
                                                    Console.ReadKey();
                                                    goto continue_quiting;
                                                case ConsoleKey.N:
                                                    goto continue_quiting;
                                                case ConsoleKey.Enter:
                                                    SaveCurrentGround();
                                                    Console.ReadKey();
                                                    goto continue_quiting;
                                            }
                                        }
                                    }
                                    continue_quiting:

                                    _context.ChangeScene(new Scene_Menu(_context));

                                    return;
                                default:
                                    Console.WriteLine($"Unknown command: {command[0]}");
                                    break;
                            }
                        }
                    }
                case ConsoleKey.N:
                    _isJustSaved = false;
                    _ground.Update();
                    break;
                case ConsoleKey.P:
                    PlaySimulation();
                    break;
                case ConsoleKey.S:
                    _isPlaying = false;
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
                    _isJustSaved = false;
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

        private void SaveCurrentGround()
        {
            while (true)
            {
                Console.WriteLine("Write path to save the ground or write 'done' to exit");
                var input = Console.ReadLine() ?? "";

                if (input.Equals("done"))
                {
                    break;
                }
                else if (!(input.Length == 0))
                {
                    _isJustSaved = _ground.SaveGround(input);

                    if (_isJustSaved)
                    {
                        Console.WriteLine("Ground was successful written.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("There is problem");
                    }
                }
            }
        }

        private void PlaySimulation()
        {
            if (_isPlaying) return;
            _currentUpdatingThread?.Join();
            _isPlaying = true;
            _isJustSaved = false;
            _currentUpdatingThread = new Thread(() => {
                while (_isPlaying)
                {
                    _isJustSaved = false;
                    _ground.Update();
                    Thread.Sleep(_delay);
                }
            });
            _currentUpdatingThread.Start();
        }
    }
}