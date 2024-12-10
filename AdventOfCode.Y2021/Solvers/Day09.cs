namespace AdventOfCode.Y2021.Solvers
{
    public class Day09 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            return GetLowestPoints(grid).Sum(point => 1 + (grid[point.Y][point.X] - '0'));
        }

        public override object SolvePart2(char[][] grid)
        {
            var sizes = new List<int>();
            foreach (var lowest in GetLowestPoints(grid))
            {
                var visited = new HashSet<Coords>();
                var queue = new Queue<Coords>();
                queue.Enqueue(lowest);
                var size = 0;
                while (queue.TryDequeue(out var point))
                {
                    if (!visited.Add(point))
                    {
                        continue;
                    }
                    size++;
                    foreach (var neighbor in point.Neighbors)
                    {
                        if (grid.IsOutOfBounds(neighbor) || visited.Contains(neighbor) || grid[neighbor.Y][neighbor.X] == '9')
                        {
                            continue;
                        }
                        queue.Enqueue(neighbor);
                    }
                }
                sizes.Add(size);
            }
            return sizes.OrderDescending().Take(3).Product();
        }

        private static List<Coords> GetLowestPoints(char[][] grid)
        {
            var lowest = new List<Coords>();
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (IsLowerThanAllNeighbors(grid, x, y))
                    {
                        lowest.Add(new(x, y));
                    }
                }
            }
            return lowest;
        }

        private static bool IsLowerThanAllNeighbors(char[][] grid, int x, int y)
        {
            var value = grid[y][x];
            return (y <= 0 || grid[y - 1][x] > value) && (x <= 0 || grid[y][x - 1] > value) &&
                (y >= grid.Length - 1 || grid[y + 1][x] > value) && (x >= grid[y].Length - 1 || grid[y][x + 1] > value);
        }
    }
}
