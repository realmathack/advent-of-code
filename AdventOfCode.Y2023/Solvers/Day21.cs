
namespace AdventOfCode.Y2023.Solvers
{
    public class Day21(int? _steps) : SolverWithLines
    {
        public Day21() : this(null) { }

        public override object SolvePart1(string[] input)
        {
            var (grid, start) = ToGrid(input);
            var steps = _steps ?? 64;
            var queue = new HashSet<Coords>() { start };
            for (int i = 0; i < steps; i++)
            {
                var next = new HashSet<Coords>();
                foreach (var current in queue)
                {
                    next.UnionWith(FindPossibleNeighbors(grid, current));
                }
                queue = next;
            }
            return queue.Count;
        }

        public override object SolvePart2(string[] input)
        {
            var (grid, start) = ToGrid(input);
            var steps = _steps ?? 26501365;
            // TODO: Implement
            return 0L;
        }

        private static List<Coords> FindPossibleNeighbors(char[][] grid, Coords node)
        {
            var neighbors = new List<Coords>();
            if (node.X > 0                       && grid[node.Left.Y][node.Left.X] == '.'  ) { neighbors.Add(node.Left); }
            if (node.Y > 0                       && grid[node.Up.Y][node.Up.X] == '.'      ) { neighbors.Add(node.Up); }
            if (node.X < grid[node.Y].Length - 1 && grid[node.Right.Y][node.Right.X] == '.') { neighbors.Add(node.Right); }
            if (node.Y < grid.Length - 1         && grid[node.Down.Y][node.Down.X] == '.'  ) { neighbors.Add(node.Down); }
            return neighbors;
        }

        private static (char[][] Grid, Coords Start) ToGrid(string[] lines)
        {
            var grid = lines.ToCharGrid();
            var start = new Coords(-1, -1);
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == 'S')
                    {
                        start = new(x, y);
                        grid[y][x] = '.';
                    }
                }
            }
            return (grid, start);
        }
    }
}
