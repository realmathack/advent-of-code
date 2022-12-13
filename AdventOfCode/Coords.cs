namespace AdventOfCode
{
    public readonly record struct Coords(int X, int Y)
    {
        public static Coords OffsetLeft => new(-1, 0);
        public static Coords OffsetRight => new(1, 0);
        public static Coords OffsetUp => new(0, -1);
        public static Coords OffsetDown => new(0, 1);
        public static Coords operator +(Coords a, Coords b) => new(a.X + b.X, a.Y + b.Y);
        public static Coords operator +(Coords a, (int x, int y) b) => new(a.X + b.x, a.Y + b.y);
        public static Coords operator -(Coords a, Coords b) => new(a.X - b.X, a.Y - b.Y);
        public static Coords operator -(Coords a, (int x, int y) b) => new(a.X - b.x, a.Y - b.y);
        public Coords Left => this + OffsetLeft;
        public Coords Right => this + OffsetRight;
        public Coords Up => this + OffsetUp;
        public Coords Down => this + OffsetDown;
        public bool IsNeighbor(Coords neighbor) => Math.Abs(neighbor.X - X) <= 1 && Math.Abs(neighbor.Y - Y) <= 1;
        /// <summary>Calculate Manhattan Distance</summary>
        public int DistanceTo(Coords target) => Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
    }
}
