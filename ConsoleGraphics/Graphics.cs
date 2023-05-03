namespace ConsoleGraphics
{
    public static class ConsoleGraphics
    {
        public static void DrawScene(Scene scene)
        {
            Console.Clear();
            scene.Draw();
        }
    }
}