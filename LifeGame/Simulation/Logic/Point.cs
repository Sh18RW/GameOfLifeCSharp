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