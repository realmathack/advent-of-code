namespace AdventOfCode.Y2021.Solvers
{
    public class Day09 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var grid = input.ToNumberGrid();
            return GetLowestPoints(grid).Sum(point => grid[point.Y][point.X] + 1);
        }

        public override object SolvePart2(string[] input)
        {
            var grid = input.ToNumberGrid();
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
                    foreach (var neighbor in FindNeighbors(grid, point))
                    {
                        if (visited.Contains(neighbor) || grid[neighbor.Y][neighbor.X] == 9)
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

        private static List<Coords> GetLowestPoints(int[][] grid)
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

        private static bool IsLowerThanAllNeighbors(int[][] grid, int x, int y)
        {
            var value = grid[y][x];
            return (y <= 0 || grid[y - 1][x] > value) && (x <= 0 || grid[y][x - 1] > value) &&
                (y >= grid.Length - 1 || grid[y + 1][x] > value) && (x >= grid[y].Length - 1 || grid[y][x + 1] > value);
        }

        private static List<Coords> FindNeighbors(int[][] grid, Coords current)
        {
            var neighbors = new List<Coords>();
            if (current.X > 0                         ) { neighbors.Add(current.Left); }
            if (current.Y > 0                         ) { neighbors.Add(current.Up); }
            if (current.X < grid[current.Y].Length - 1) { neighbors.Add(current.Right); }
            if (current.Y < grid.Length - 1           ) { neighbors.Add(current.Down); }
            return neighbors;
        }
    }
}
