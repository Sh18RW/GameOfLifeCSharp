using System.Text;
using LifeGame.Simulation;

namespace LifeGame.Graphics.Scenes
{
    public class Scene_LoadGround : Scene
    {
        private int _selection;
        public Scene_LoadGround(IContext context) : base(context)
        {
            _selection = 0;
        }

        private protected override string Draw()
        {
            var result = new StringBuilder();

            result.Append("Enter world name");

            if (_selection == 0)
            {
                result.Insert(0, "> ");
                result.Append(" <");
            }

            result.Append('\n');

            var line = new StringBuilder();

            line.Append("Back");

            if (_selection == 1)
            {
                line.Insert(0, "> ");
                line.Append(" <");
            }

            result.Append(line);

            return result.ToString();
        }

        private protected override void UpdateControl()
        {
            var key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (--_selection < 0)
                    {
                        _selection += 2;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (++_selection >= 2)
                    {
                        _selection -= 2;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (_selection == 0){
                        Console.Write("Enter full path to serialized gorund: ");

                        var ground = Ground.LoadGround(Console.ReadLine() ?? "");

                        if (ground == null)
                        {
                            Console.WriteLine("Something wrong in ground loading!");
                            Console.ReadKey();
                            break;
                        }

                        Environment.Exit(-1);
                    }
                    else
                    {
                        _context.ChangeScene(new Scene_Menu(_context));
                    }

                    break;
            }
        }
    }
}