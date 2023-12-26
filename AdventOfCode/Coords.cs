namespace AdventOfCode
{
    public readonly record struct Coords(int X, int Y)
    {
        public static Coords OffsetLeft => new(-1, 0);
        public static Coords OffsetRight => new(1, 0);
        public static Coords OffsetUp => new(0, -1);
        public static Coords OffsetDown => new(0, 1);
        public static Coords OffsetUpLeft => new(-1, -1);
        public static Coords OffsetUpRight => new(1, -1);
        public static Coords OffsetDownLeft => new(-1, 1);
        public static Coords OffsetDownRight => new(1, 1);
        public static Coords operator +(Coords a, Coords b) => new(a.X + b.X, a.Y + b.Y);
        public static Coords operator +(Coords a, (int X, int Y) b) => new(a.X + b.X, a.Y + b.Y);
        public static Coords operator -(Coords a, Coords b) => new(a.X - b.X, a.Y - b.Y);
        public static Coords operator -(Coords a, (int X, int Y) b) => new(a.X - b.X, a.Y - b.Y);
        public static Coords operator *(Coords a, int multiplier) => new(a.X * multiplier, a.Y * multiplier);
        public Coords Left => new(X - 1, Y);
        public Coords Right => new(X + 1, Y);
        public Coords Up => new(X, Y - 1);
        public Coords Down => new(X, Y + 1);
        public Coords UpLeft => new(X - 1, Y - 1);
        public Coords UpRight => new(X + 1, Y - 1);
        public Coords DownLeft => new(X - 1, Y + 1);
        public Coords DownRight => new(X + 1, Y + 1);
        /// <summary>Horizontal & Vertical</summary>
        public Coords[] Neighbors => new[] { Left, Right, Up, Down };
        /// <summary>Horizontal, Vertical & Diagonal</summary>
        public Coords[] Adjacents => new[] { Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight };
        /// <summary>Adjacent or on top</summary>
        public bool IsAdjacent(Coords other) => Math.Abs(other.X - X) <= 1 && Math.Abs(other.Y - Y) <= 1;
        /// <summary>Calculate Manhattan Distance</summary>
        public int DistanceTo(Coords other) => Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
        /// <summary>Calculate Offset/Direction to target</summary>
        public Coords OffsetTo(Coords other) => new((other.X - X == 0) ? 0 : (other.X - X < 0) ? -1 : 1, (other.Y - Y == 0) ? 0 : (other.Y - Y < 0) ? -1 : 1);
        public Coords RotateLeft => new(Y, -X);
        public Coords RotateRight => new(-Y, X);
        public override string ToString() => $"{X},{Y}";
    }
}
