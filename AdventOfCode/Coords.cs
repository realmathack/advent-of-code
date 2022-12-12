namespace AdventOfCode
{
    public record struct Coords(int X, int Y)
    {
        public static Coords operator +(Coords a, Coords b) => new(a.X + b.X, a.Y + b.Y);
        public static Coords operator -(Coords a, Coords b) => new(a.X - b.X, a.Y - b.Y);
        public bool IsNeighbor(Coords neighbor) => Math.Abs(neighbor.X - X) <= 1 && Math.Abs(neighbor.Y - Y) <= 1;
    }
}
