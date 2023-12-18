namespace AdventOfCode
{
    public readonly record struct Coords(int X, int Y)
    {
        public static Coords OffsetLeft => new(-1, 0);
        public static Coords OffsetUp => new(0, -1);
        public static Coords OffsetRight => new(1, 0);
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
        public Coords Left => this + OffsetLeft;
        public Coords Up => this + OffsetUp;
        public Coords Right => this + OffsetRight;
        public Coords Down => this + OffsetDown;
        public Coords UpLeft => this + OffsetUpLeft;
        public Coords UpRight => this + OffsetUpRight;
        public Coords DownLeft => this + OffsetDownLeft;
        public Coords DownRight => this + OffsetDownRight;
        /// <summary>Horizontal & Vertical</summary>
        public Coords[] Neighbors => new[] { Left, Up, Right, Down };
        /// <summary>Horizontal, Vertical & Diagonal</summary>
        public Coords[] Adjacents => new[] { Left, UpLeft, Up, UpRight, Right, DownRight, Down, DownLeft };
        public bool IsNeighbor(Coords neighbor) => Math.Abs(neighbor.X - X) <= 1 && Math.Abs(neighbor.Y - Y) <= 1;
        /// <summary>Calculate Manhattan Distance</summary>
        public int DistanceTo(Coords target) => Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
        /// <summary>Calculate Offset/Direction to target</summary>
        public Coords OffsetTo(Coords target) => new((target.X - X == 0) ? 0 : (target.X - X < 0) ? -1 : 1, (target.Y - Y == 0) ? 0 : (target.Y - Y < 0) ? -1 : 1);
        public Coords RotateLeft => new(Y, -X);
        public Coords RotateRight => new(-Y, X);
        public override string ToString() => $"{X}, {Y}";
    }
}
