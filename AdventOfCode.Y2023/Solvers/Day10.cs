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
            var (grid, start) = ToGrid(input);
            var loop = GetLoop(grid, start);
            IsolateLoop(grid, loop);
            var enclosed = 0;
            for (int y = 0; y < grid.Length; y++)
            {
                var crossings = 0;
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (_oddEvenWalls.Contains(grid[y][x]))
                    {
                        crossings++;
                    }
                    else if (crossings % 2 == 1 && grid[y][x] == '.')
                    {
                        enclosed++;
                    }
                }
            }
            return enclosed;
        }

        // https://en.wikipedia.org/wiki/Even%E2%80%93odd_rule
        private static readonly char[] _oddEvenWalls = ['|', 'J', 'L'];

        private static void IsolateLoop(char[][] grid, HashSet<Coords> loop)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (!loop.Contains(new(x, y)))
                    {
                        grid[y][x] = '.';
                    }
                }
            }
        }

        private static HashSet<Coords> GetLoop(char[][] grid, Coords current)
        {
            var visited = new HashSet<Coords>();
            var loop = new HashSet<Coords>();
            while (visited.Add(current))
            {
                loop.Add(current);
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
            ["north"] = ['|', 'L', 'J'],
            ["east"]  = ['-', 'L', 'F'],
            ["south"] = ['|', '7', 'F'],
            ["west"]  = ['-', '7', 'J']
        };
        private static List<Coords> FindPossibleNeighbors(char[][] grid, Coords node)
        {
            var neighbors = new List<Coords>();
            var currentPipe = grid[node.Y][node.X];
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
            int x;
            for (int y = 0; y < lines.Length; y++)
            {
                grid[y] = lines[y].ToCharArray();
                if (start is null && (x = lines[y].IndexOf('S')) != -1)
                {
                    start = new Coords(x, y);
                    if (start.Value.Left.X > 0 && _pipes["east"].Contains(grid[start.Value.Left.Y][start.Value.Left.X]))
                    {
                        grid[y][x] = (start.Value.Up.Y > 0 && _pipes["south"].Contains(grid[start.Value.Up.Y][start.Value.Up.X])) ? 'J' : '7';
                    }
                    else
                    {
                        grid[y][x] = (start.Value.Up.Y > 0 && _pipes["south"].Contains(grid[start.Value.Up.Y][start.Value.Up.X])) ? 'L' : 'F';
                    }
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
