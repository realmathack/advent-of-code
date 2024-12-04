namespace AdventOfCode
{
    public readonly record struct Coords3D(int X, int Y, int Z)
    {
        public static Coords3D OffsetLeft => new(-1, 0, 0);
        public static Coords3D OffsetRight => new(1, 0, 0);
        public static Coords3D OffsetForward => new(0, -1, 0);
        public static Coords3D OffsetBack => new(0, 1, 0);
        public static Coords3D OffsetDown => new(0, 0, -1);
        public static Coords3D OffsetUp => new(0, 0, 1);
        public static Coords3D operator +(Coords3D a, Coords3D b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Coords3D operator +(Coords3D a, (int X, int Y, int Z) b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Coords3D operator -(Coords3D a, Coords3D b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Coords3D operator -(Coords3D a, (int X, int Y, int Z) b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public Coords3D Left => new(X - 1, Y, Z);
        public Coords3D Right => new(X + 1, Y, Z);
        public Coords3D Forward => new(X, Y - 1, Z);
        public Coords3D Back => new(X, Y + 1, Z);
        public Coords3D Down => new(X, Y, Z - 1);
        public Coords3D Up => new(X, Y, Z + 1);
        public Coords3D[] Neighbors => [Left, Right, Forward, Back, Down, Up];
        public override string ToString() => $"{X},{Y},{Z}";
    }
}
