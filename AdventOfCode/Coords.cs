using System.Numerics;

namespace AdventOfCode
{
    public readonly record struct Coords<T>(T X, T Y)
        where T : struct, INumber<T>
    {
        public static Coords<T> OffsetLeft => new(-T.One, T.Zero);
        public static Coords<T> OffsetRight => new(T.One, T.Zero);
        public static Coords<T> OffsetUp => new(T.Zero, -T.One);
        public static Coords<T> OffsetDown => new(T.Zero, T.One);
        public static Coords<T> OffsetUpLeft => new(-T.One, -T.One);
        public static Coords<T> OffsetUpRight => new(T.One, -T.One);
        public static Coords<T> OffsetDownLeft => new(-T.One, T.One);
        public static Coords<T> OffsetDownRight => new(T.One, T.One);
        /// <summary>Horizontal & Vertical</summary>
        public static Coords<T>[] NeighborOffsets => [OffsetLeft, OffsetRight, OffsetUp, OffsetDown];
        /// <summary>Horizontal, Vertical & Diagonal</summary>
        public static Coords<T>[] AdjacentOffsets => [OffsetLeft, OffsetRight, OffsetUp, OffsetDown, OffsetUpLeft, OffsetUpRight, OffsetDownLeft, OffsetDownRight];
        public static Coords<T> operator +(Coords<T> a, Coords<T> b) => new(a.X + b.X, a.Y + b.Y);
        public static Coords<T> operator +(Coords<T> a, (T X, T Y) b) => new(a.X + b.X, a.Y + b.Y);
        public static Coords<T> operator -(Coords<T> a, Coords<T> b) => new(a.X - b.X, a.Y - b.Y);
        public static Coords<T> operator -(Coords<T> a, (T X, T Y) b) => new(a.X - b.X, a.Y - b.Y);
        public static Coords<T> operator *(Coords<T> a, T multiplier) => new(a.X * multiplier, a.Y * multiplier);
        public Coords<T> Left => new(X - T.One, Y);
        public Coords<T> Right => new(X + T.One, Y);
        public Coords<T> Up => new(X, Y - T.One);
        public Coords<T> Down => new(X, Y + T.One);
        public Coords<T> UpLeft => new(X - T.One, Y - T.One);
        public Coords<T> UpRight => new(X + T.One, Y - T.One);
        public Coords<T> DownLeft => new(X - T.One, Y + T.One);
        public Coords<T> DownRight => new(X + T.One, Y + T.One);
        /// <summary>Horizontal & Vertical</summary>
        public Coords<T>[] Neighbors => [Left, Right, Up, Down];
        /// <summary>Horizontal, Vertical & Diagonal</summary>
        public Coords<T>[] Adjacents => [Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight];
        /// <summary>Adjacent or on top</summary>
        public bool IsAdjacent(Coords<T> other) => T.Abs(other.X - X) <= T.One && T.Abs(other.Y - Y) <= T.One;
        /// <summary>Calculate Manhattan Distance</summary>
        public T DistanceTo(Coords<T> other) => T.Abs(other.X - X) + T.Abs(other.Y - Y);
        /// <summary>Calculate Offset/Direction to target</summary>
        public Coords<T> OffsetTo(Coords<T> other) => new((other.X - X == T.Zero) ? T.Zero : (other.X - X < T.Zero) ? -T.One : T.One, (other.Y - Y == T.Zero) ? T.Zero : (other.Y - Y < T.Zero) ? -T.One : T.One);
        public Coords<T> RotateLeft => new(Y, -X);
        public Coords<T> RotateRight => new(-Y, X);
        public override string ToString() => $"{X},{Y}";
        public static Coords<T> Parse(string s)
        {
            var coords = s.Split(',', 2);
            return new(T.Parse(coords[0], null), T.Parse(coords[1], null));
        }
    }
}
