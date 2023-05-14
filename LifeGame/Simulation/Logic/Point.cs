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
            var xPosElement = element.GetElementsByTagName("x")[0];
            var yPosElement = element.GetElementsByTagName("y")[0];

            var xPosText = (XmlText) xPosElement.FirstChild;
            var yPosText = (XmlText) yPosElement.FirstChild;

            return new Point(int.Parse(xPosText.Value), y: int.Parse(yPosText.Value));
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
    }
}