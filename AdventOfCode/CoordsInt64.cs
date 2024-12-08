namespace AdventOfCode
{
    public readonly record struct CoordsInt64(long X, long Y)
    {
        public static CoordsInt64 OffsetLeft => new(-1L, 0L);
        public static CoordsInt64 OffsetRight => new(1L, 0L);
        public static CoordsInt64 OffsetUp => new(0L, -1L);
        public static CoordsInt64 OffsetDown => new(0L, 1L);
        public static CoordsInt64 OffsetUpLeft => new(-1L, -1L);
        public static CoordsInt64 OffsetUpRight => new(1L, -1L);
        public static CoordsInt64 OffsetDownLeft => new(-1L, 1L);
        public static CoordsInt64 OffsetDownRight => new(1L, 1L);
        /// <summary>Horizontal & Vertical</summary>
        public static CoordsInt64[] NeighborOffsets => [OffsetLeft, OffsetRight, OffsetUp, OffsetDown];
        /// <summary>Horizontal, Vertical & Diagonal</summary>
        public static CoordsInt64[] AdjacentOffsets => [OffsetLeft, OffsetRight, OffsetUp, OffsetDown, OffsetUpLeft, OffsetUpRight, OffsetDownLeft, OffsetDownRight];
        public static CoordsInt64 operator +(CoordsInt64 a, CoordsInt64 b) => new(a.X + b.X, a.Y + b.Y);
        public static CoordsInt64 operator +(CoordsInt64 a, (long X, long Y) b) => new(a.X + b.X, a.Y + b.Y);
        public static CoordsInt64 operator -(CoordsInt64 a, CoordsInt64 b) => new(a.X - b.X, a.Y - b.Y);
        public static CoordsInt64 operator -(CoordsInt64 a, (long X, long Y) b) => new(a.X - b.X, a.Y - b.Y);
        public static CoordsInt64 operator *(CoordsInt64 a, long multiplier) => new(a.X * multiplier, a.Y * multiplier);
        public CoordsInt64 Left => new(X - 1L, Y);
        public CoordsInt64 Right => new(X + 1L, Y);
        public CoordsInt64 Up => new(X, Y - 1L);
        public CoordsInt64 Down => new(X, Y + 1L);
        public CoordsInt64 UpLeft => new(X - 1L, Y - 1L);
        public CoordsInt64 UpRight => new(X + 1L, Y - 1L);
        public CoordsInt64 DownLeft => new(X - 1L, Y + 1L);
        public CoordsInt64 DownRight => new(X + 1L, Y + 1L);
        /// <summary>Horizontal & Vertical</summary>
        public CoordsInt64[] Neighbors => [Left, Right, Up, Down];
        /// <summary>Horizontal, Vertical & Diagonal</summary>
        public CoordsInt64[] Adjacents => [Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight];
        /// <summary>Adjacent or on top</summary>
        public bool IsAdjacent(CoordsInt64 other) => Math.Abs(other.X - X) <= 1L && Math.Abs(other.Y - Y) <= 1L;
        /// <summary>Calculate Manhattan Distance</summary>
        public long DistanceTo(CoordsInt64 other) => Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
        /// <summary>Calculate Offset/Direction to target</summary>
        public CoordsInt64 OffsetTo(CoordsInt64 other) => new((other.X - X == 0L) ? 0L : (other.X - X < 0L) ? -1L : 1L, (other.Y - Y == 0L) ? 0L : (other.Y - Y < 0L) ? -1L : 1L);
        public CoordsInt64 RotateLeft => new(Y, -X);
        public CoordsInt64 RotateRight => new(-Y, X);
        public override string ToString() => $"{X},{Y}";
    }
}
