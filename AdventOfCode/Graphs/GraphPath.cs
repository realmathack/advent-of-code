namespace AdventOfCode.Graphs
{
    public readonly record struct GraphPath<T>(List<T> Nodes, int Distance)
    {
        private static readonly GraphPath<T> _empty = new([], int.MinValue);
        public static GraphPath<T> Empty => _empty;
    }
}
