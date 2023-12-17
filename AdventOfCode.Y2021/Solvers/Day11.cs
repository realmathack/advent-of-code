namespace AdventOfCode.Y2021.Solvers
{
    public class Day11 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var sum = 0;
            var grid = input.ToNumberGrid();
            for (int i = 0; i < 100; i++)
            {
                sum += ExecuteStep(grid);
            }
            return sum;
        }

        public override object SolvePart2(string[] input)
        {
            var grid = input.ToNumberGrid();
            var cellCount = grid.Length * grid[0].Length;
            for (int i = 0; i < 1_000_000; i++)
            {
                if (ExecuteStep(grid) == cellCount)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        private static int ExecuteStep(int[][] grid)
        {
            var flashed = new HashSet<Coords>();
            var queue = new Queue<Coords>();
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x]++ == 9)
                    {
                        queue.Enqueue(new(x, y));
                    }
                }
            }
            while (queue.TryDequeue(out var current))
            {
                foreach (var octopus in current.Adjacents)
                {
                    if (grid.IsOutOfBounds(octopus))
                    {
                        continue;
                    }
                    if (grid[octopus.Y][octopus.X]++ == 9 && !flashed.Contains(octopus))
                    {
                        queue.Enqueue(octopus);
                    }
                }
                flashed.Add(current);
            }
            foreach (var octopus in flashed)
            {
                grid[octopus.Y][octopus.X] = 0;
            }
            return flashed.Count;
        }
    }
}
