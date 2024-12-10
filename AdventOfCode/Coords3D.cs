using System.Numerics;

namespace AdventOfCode
{
    public readonly record struct Coords3D<T>(T X, T Y, T Z)
        where T : struct, INumber<T>
    {
        public static Coords3D<T> OffsetLeft => new(-T.One, T.Zero, T.Zero);
        public static Coords3D<T> OffsetRight => new(T.One, T.Zero, T.Zero);
        public static Coords3D<T> OffsetForward => new(T.Zero, -T.One, T.Zero);
        public static Coords3D<T> OffsetBack => new(T.Zero, T.One, T.Zero);
        public static Coords3D<T> OffsetDown => new(T.Zero, T.Zero, -T.One);
        public static Coords3D<T> OffsetUp => new(T.Zero, T.Zero, T.One);
        public static Coords3D<T> operator +(Coords3D<T> a, Coords3D<T> b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Coords3D<T> operator +(Coords3D<T> a, (T X, T Y, T Z) b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Coords3D<T> operator -(Coords3D<T> a, Coords3D<T> b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Coords3D<T> operator -(Coords3D<T> a, (T X, T Y, T Z) b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public Coords3D<T> Left => new(X - T.One, Y, Z);
        public Coords3D<T> Right => new(X + T.One, Y, Z);
        public Coords3D<T> Forward => new(X, Y - T.One, Z);
        public Coords3D<T> Back => new(X, Y + T.One, Z);
        public Coords3D<T> Down => new(X, Y, Z - T.One);
        public Coords3D<T> Up => new(X, Y, Z + T.One);
        public Coords3D<T>[] Neighbors => [Left, Right, Forward, Back, Down, Up];
        public override string ToString() => $"{X},{Y},{Z}";
    }
}
