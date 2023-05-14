using System.Xml;

namespace LifeGame.Simulation.Logic
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public XmlNode MakeXmlNode(XmlDocument root)
        {

            var pointRoot = root.CreateElement("point");

            var xCoordXmlElement = root.CreateElement("x");
            var yCoordXmlElement = root.CreateElement("y");

            var xCoord = root.CreateTextNode(X.ToString());
            var yCoord = root.CreateTextNode(Y.ToString());

            xCoordXmlElement.AppendChild(xCoord);
            yCoordXmlElement.AppendChild(yCoord);

            pointRoot.AppendChild(xCoordXmlElement);
            pointRoot.AppendChild(yCoordXmlElement);

            return pointRoot;
        }

        public static Point MakePointFromXml(XmlElement element)
        {
            return new Point(ParsePos(element, "x"), ParsePos(element, "y"));
        }

        public static implicit operator Point(int x)
        {
            return new Point(x, x);
        }

        public static implicit operator Point((int x, int y) a)
        {
            return new Point(a.x, a.y);
        }

        public override bool Equals(object? obj)
        {
            return obj != null && obj is Point point && point.X == X && point.Y == Y;
        }
        
        public override int GetHashCode()
        {
            return X + Y;
        }

        private static int ParsePos(XmlElement element, string positionName)
        {
            try
            {
                var poses = element.GetElementsByTagName(positionName);

                if (poses.Count == 0)
                    throw new Exception();

                var positionElement = (poses[0] ?? throw new Exception()).FirstChild ?? throw new Exception();
                var positionText = positionElement as XmlText ?? throw new Exception();
                if (!int.TryParse(positionText.Value, out var position)) throw new Exception();

                return position;
            }
            catch(Exception)
            {
                throw new Exception("Failed on position parsing");
            }
        }
    }
}