namespace AdventOfCode.Y2023.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (grid, start) = ToGrid(input);
            return GetLoop(grid, start).Count / 2;
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Implement
            //var (grid, start) = ToGrid(input);
            //var loop = GetLoop(grid, start);
            //loop.Add(start);
            return null!;
        }

        private static List<Coords> GetLoop(char[][] grid, Coords current)
        {
            var visited = new HashSet<Coords>();
            var loop = new List<Coords>();
            while (!visited.Contains(current))
            {
                loop.Add(current);
                visited.Add(current);
                foreach (var neighbor in FindPossibleNeighbors(grid, current).Where(neighbor => !visited.Contains(neighbor)))
                {
                    current = neighbor;
                    break;
                }
            }
            return loop;
        }

        private static readonly Dictionary<string, char[]> _pipes = new()
        {
            { "north", ['|', 'L', 'J'] },
            { "east",  ['-', 'L', 'F'] },
            { "south", ['|', '7', 'F'] },
            { "west",  ['-', '7', 'J'] }
        };
        private static List<Coords> FindPossibleNeighbors(char[][] grid, Coords node)
        {
            var neighbors = new List<Coords>();
            var currentPipe = grid[node.Y][node.X];
            if (currentPipe == 'S')
            {
                var right = node.Right;
                var down = node.Down;
                var left = node.Left;
                var up = node.Up;
                if (right.X < grid[right.Y].Length - 1 && _pipes["west"].Contains(grid[right.Y][right.X])) { neighbors.Add(right); }
                if (down.Y < grid.Length - 1           && _pipes["north"].Contains(grid[down.Y][down.X]) ) { neighbors.Add(down); }
                if (left.X > 0                         && _pipes["east"].Contains(grid[left.Y][left.X])  ) { neighbors.Add(left); }
                if (up.Y > 0                           && _pipes["south"].Contains(grid[up.Y][up.X])     ) { neighbors.Add(up); }
                return neighbors;
            }
            if (node.X > 0                       && _pipes["west"].Contains(currentPipe) ) { neighbors.Add(node.Left); }
            if (node.Y > 0                       && _pipes["north"].Contains(currentPipe)) { neighbors.Add(node.Up); }
            if (node.X < grid[node.Y].Length - 1 && _pipes["east"].Contains(currentPipe) ) { neighbors.Add(node.Right); }
            if (node.Y < grid.Length - 1         && _pipes["south"].Contains(currentPipe)) { neighbors.Add(node.Down); }
            return neighbors;
        }

        private static (char[][] Grid, Coords Start) ToGrid(string[] lines)
        {
            var grid = new char[lines.Length][];
            Coords? start = null;
            int col;
            for (int row = 0; row < lines.Length; row++)
            {
                grid[row] = lines[row].ToCharArray();
                if (start is null && (col = lines[row].IndexOf('S')) != -1)
                {
                    start = new Coords(col, row);
                }
            }
            if (start is null)
            {
                throw new InvalidOperationException("Start not found!");
            }
            return (grid, start.Value);
        }
    }
}
