namespace AdventOfCode.Y2024.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (grid, trailheads) = ToGrid(input);
            var results = new Dictionary<Coords, HashSet<Coords>>();
            foreach (var trailhead in trailheads)
            {
                FindTops(grid, trailhead, results);
            }
            var sum = 0;
            foreach (var trailhead in trailheads)
            {
                sum += results[trailhead].Count;
            }
            return sum;
        }

        public override object SolvePart2(string[] input)
        {
            var (grid, trailheads) = ToGrid(input);
            var results = new Dictionary<Coords, int>();
            foreach (var trailhead in trailheads)
            {
                FindTrails(grid, trailhead, results);
            }
            var sum = 0;
            foreach (var trailhead in trailheads)
            {
                sum += results[trailhead];
            }
            return sum;
        }

        private static void FindTops(char[][] grid, Coords current, Dictionary<Coords, HashSet<Coords>> results)
        {
            var result = new HashSet<Coords>();
            var currentHeight = grid[current.Y][current.X];
            foreach (var neighbor in current.Neighbors)
            {
                if (grid.IsOutOfBounds(neighbor))
                {
                    continue;
                }
                var neighborHeight = grid[neighbor.Y][neighbor.X];
                if (neighborHeight - currentHeight != 1)
                {
                    continue;
                }
                if (neighborHeight == '9')
                {
                    result.Add(neighbor);
                    continue;
                }
                if (!results.ContainsKey(neighbor))
                {
                    FindTops(grid, neighbor, results);
                }
                result.UnionWith(results[neighbor]);
            }
            results[current] = result;
        }

        private static void FindTrails(char[][] grid, Coords current, Dictionary<Coords, int> results)
        {
            var result = 0;
            var currentHeight = grid[current.Y][current.X];
            foreach (var neighbor in current.Neighbors)
            {
                if (grid.IsOutOfBounds(neighbor))
                {
                    continue;
                }
                var neighborHeight = grid[neighbor.Y][neighbor.X];
                if (neighborHeight - currentHeight != 1)
                {
                    continue;
                }
                if (neighborHeight == '9')
                {
                    result++;
                    continue;
                }
                if (!results.ContainsKey(neighbor))
                {
                    FindTrails(grid, neighbor, results);
                }
                result += results[neighbor];
            }
            results[current] = result;
        }

        private static (char[][] Grid, List<Coords> Trailheads) ToGrid(string[] lines)
        {
            var grid = lines.ToCharGrid();
            var trailheads = new List<Coords>();
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == '0')
                    {
                        trailheads.Add(new(x, y));
                    }
                }
            }
            return (grid,  trailheads);
        }
    }
}
