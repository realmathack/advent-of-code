namespace AdventOfCode.Y2024.Solvers
{
    public class Day10 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            var trailheads = GetTrailheads(grid);
            var results = new Dictionary<Coords, HashSet<Coords>>();
            trailheads.ForEach(trailhead => FindTops(grid, trailhead, results));
            return trailheads.Sum(trailhead => results[trailhead].Count);
        }

        public override object SolvePart2(char[][] grid)
        {
            var trailheads = GetTrailheads(grid);
            var results = new Dictionary<Coords, int>();
            trailheads.ForEach(trailhead => FindTrails(grid, trailhead, results));
            return trailheads.Sum(trailhead => results[trailhead]);
        }

        private static void FindTops(char[][] grid, Coords current, Dictionary<Coords, HashSet<Coords>> results)
        {
            var result = new HashSet<Coords>();
            foreach (var neighbor in FindNeighbors(grid, current))
            {
                if (grid[neighbor.Y][neighbor.X] == '9')
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
            foreach (var neighbor in FindNeighbors(grid, current))
            {
                if (grid[neighbor.Y][neighbor.X] == '9')
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

        private static List<Coords> FindNeighbors(char[][] grid, Coords current)
        {
            var neighbors = new List<Coords>();
            var currentHeight = grid[current.Y][current.X];
            if (current.X > 0                          && (grid[current.Y][current.X - 1] - currentHeight) == 1) { neighbors.Add(current.Left); }
            if (current.Y > 0                          && (grid[current.Y - 1][current.X] - currentHeight) == 1) { neighbors.Add(current.Up); }
            if (current.X < grid[current.Y].Length - 1 && (grid[current.Y][current.X + 1] - currentHeight) == 1) { neighbors.Add(current.Right); }
            if (current.Y < grid.Length - 1            && (grid[current.Y + 1][current.X] - currentHeight) == 1) { neighbors.Add(current.Down); }
            return neighbors;
        }

        private static List<Coords> GetTrailheads(char[][] grid)
        {
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
            return trailheads;
        }
    }
}
