using System.Text;

namespace LifeGame.Graphics.Scenes
{
    public class Scene_NewGround : Scene
    {
        private int _currentVerticalPos, _currentHorizontalPos;
        private int _selectedWorldType, _worldSize;

        public Scene_NewGround(IContext context) : base(context)
        {
            _currentVerticalPos = 0;
            _currentHorizontalPos = 0;
            _selectedWorldType = 0;
            _worldSize = 10;
        }

        private protected override string Draw()
        {
            var result = new StringBuilder();

            result.Append("Const world size");

            if (_selectedWorldType == 0)
            {
                result.Append(" (selected)");
            }

            if (_currentVerticalPos == 0 && _currentHorizontalPos == 0)
            {
                result.Insert(0, "> ");
                result.Append(" <");
            }

            result.Append("\t\t\t");

            if (_selectedWorldType == 0)
            {
                var line = new StringBuilder();

                line.Append($"{_worldSize}x{_worldSize}");

                if (_currentVerticalPos == 0 && _currentHorizontalPos == 1)
                {
                    line.Insert(0, "> ");
                    line.Append(" <");
                }

                result.Append(line);
            }
            else
            {
                result.Append("const");
            }

            result.Append('\n');

            {
                var line = new StringBuilder();

                line.Append("Extended World");

                if (_selectedWorldType == 1)
                {
                    line.Append(" (selected)");
                }

                if (_currentVerticalPos == 1)
                {
                    line.Insert(0, "> ");
                    line.Append(" <");
                }

                result.Append(line);
            }

            result.Append('\n');

            {
                var line = new StringBuilder();

                line.Append(value: "Done");

                if (_currentVerticalPos == 2 && _currentHorizontalPos == 0)
                {
                    line.Insert(0, "> ");
                    line.Append(" <");
                }

                result.Append(line);
            }

            result.Append("\t\t");

            {
                var line = new StringBuilder();

                line.Append("Back");

                if (_currentVerticalPos == 2 && _currentHorizontalPos == 1)
                {
                    line.Insert(0, "> ");
                    line.Append(" <");
                }

                result.Append(line);
            }

            return result.ToString();
        }

        private protected override void UpdateControl()
        {
            var key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (--_currentVerticalPos < 0)
                    {
                        _currentVerticalPos += 3;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (++_currentVerticalPos > 2)
                    {
                        _currentVerticalPos -= 3;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (--_currentHorizontalPos < 0)
                    {
                        _currentHorizontalPos += 2;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (++_currentHorizontalPos > 1)
                    {
                        _currentHorizontalPos -= 2;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (_currentHorizontalPos == 0)
                    {
                        switch (_currentVerticalPos)
                        {
                            case 0:
                                _selectedWorldType = 0;
                                break;
                            case 1:
                                _selectedWorldType = 1;
                                break;
                            case 2:
                                Environment.Exit(-1);
                                break;
                        }
                    }
                    else
                    {
                        switch (_currentVerticalPos)
                        {
                            case 0:
                                if (_selectedWorldType != 0) break;
                                Console.Write("Enter new world size: ");
                                if(int.TryParse(Console.ReadLine(), out int worldSize))
                                {
                                    if (worldSize < 10)
                                    {
                                        Console.WriteLine("World size mustn't be lower than 10");
                                        Console.ReadKey();
                                        break;
                                    }

                                    _worldSize = worldSize;
                                }
                                else
                                {
                                    Console.WriteLine("Can't parse int (new world size as number)");
                                    Console.ReadKey();
                                }
                                break;
                            case 1:
                                _selectedWorldType = 1;
                                break;
                            case 2:
                                _context.ChangeScene(new Scene_Menu(_context));
                                break;
                        }
                    }
                    break;
            }
        }
    }
}