using System.Text;

namespace LifeGame.Graphics.Scenes
{
    public class Scene_Menu : LifeGame.Graphics.Scene
    {
        private static readonly string[] menuItems = new string[]{ "New Ground", "Load Ground", "Shut Down" };
        private int _currentMenu;

        public Scene_Menu()
        {
            _currentMenu = 0;
        }

        private protected override string Draw()
        {
            var result = new StringBuilder();

            result.Append("\t\t\tLife Game by Sh18RW\n");

            result.Append(DrawMenuItems());

            return result.ToString();
        }

        private protected override void UpdateControl()
        {
            var key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (--_currentMenu < 0)
                    {
                        _currentMenu += menuItems.Length;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (++_currentMenu >= menuItems.Length)
                    {
                        _currentMenu -= menuItems.Length;
                    }
                    break;
            }
        }

        private string DrawMenuItems()
        {
            var result = new StringBuilder();

            for (int i = 0;i < menuItems.Length;i++)
            {
                result.Append(DrawItem(i));
                result.Append('\n');
            }

            return result.ToString();
        }

        private string DrawItem(int id)
        {
            var result = new StringBuilder();
            
            result.Append(menuItems[id]);

            if (id == _currentMenu)
            {
                result.Insert(0, "> ");
                result.Append(" <");
            }

            return result.ToString();
        }
    }
}