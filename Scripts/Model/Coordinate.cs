namespace Model
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate(Coordinate coordinate)
        {
            X = coordinate.X;
            Y = coordinate.Y;
        }

        public static Coordinate operator -(Coordinate a, Coordinate b) => new Coordinate(a.X - b.X, a.Y - b.Y);

        public bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override string ToString()
        {
            return X + ": " + Y;
        }
    }
}