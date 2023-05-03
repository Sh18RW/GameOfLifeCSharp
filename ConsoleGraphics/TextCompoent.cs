namespace ConsoleGraphics
{
    public class TextComponent : Component
    {
        private string _text;

        public TextComponent() : this("") {}

        public TextComponent(string text)
        {
            _text = text;
        }

        public override void AddComponent(Component component)
        {
            throw new Exception("TextComponent mustn't have any childer-components!");
        }

        internal override string Draw()
        {
            return _text;
        }
    }
}