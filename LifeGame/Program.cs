using LifeGame.Graphics.Scenes;

namespace LifeGame
{
    public static class Program
    {
        public static void Main()
        {
            var scene = new Scene_Menu();

            while (true)
            {
                scene.Update();
            }
        }
    }
}