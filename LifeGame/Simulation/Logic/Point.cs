using System.Xml;

namespace LifeGame.Simulation.Logic
{
    [Serializable]
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
            // I HATE SECURITY!
            _ = int.TryParse(element.GetAttribute("x"), out int xPos);
            _ = int.TryParse(element.GetAttribute("y"), out int yPos);
            // I REALLY HATE IT. I ONLY WANT TO SEE THIS PROGRAM WORKING!!!!!!!!!!!

            return new Point(xPos, yPos);
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