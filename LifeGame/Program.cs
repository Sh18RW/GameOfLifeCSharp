using LifeGame.Graphics;
using LifeGame.Graphics.Scenes;

namespace LifeGame
{
    public static class Program
    {
        public static void Main()
        {
            new LifeGame().Start();
        }

        public class LifeGame : IContext
        {
            private Scene _currentScene;

            public LifeGame()
            {
                _currentScene = new Scene_Menu(this);
            }

            public void Start()
            {
                while (true)
                {
                    _currentScene.Update();
                }
            }

            public void ChangeScene(Scene scene)
            {
                _currentScene = scene;
            }
        }
    }
}