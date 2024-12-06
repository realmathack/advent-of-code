namespace AdventOfCode.Y2024.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (grid, current) = ToGrid(input);
            var direction = Coords.OffsetUp;
            var visited = new HashSet<Coords>();
            while (true)
            {
                visited.Add(current);
                var next = current + direction;
                if (grid.IsOutOfBounds(next))
                {
                    break;
                }
                if (grid[next.Y][next.X] == '#')
                {
                    direction = direction.RotateRight;
                    continue;
                }
                current = next;
            }
            return visited.Count;
        }

        public override object SolvePart2(string[] input)
        {
            var (grid, start) = ToGrid(input);
            var current = start;
            var direction = Coords.OffsetUp;
            var loopObstacles = new HashSet<Coords>();
            while (true)
            {
                var next = current + direction;
                if (grid.IsOutOfBounds(next))
                {
                    break;
                }
                if (grid[next.Y][next.X] == '#')
                {
                    direction = direction.RotateRight;
                    continue;
                }
                grid[next.Y][next.X] = '#';
                if (HasLoop(grid, start, Coords.OffsetUp))
                {
                    loopObstacles.Add(next);
                }
                grid[next.Y][next.X] = '.';
                current = next;
            }
            return loopObstacles.Count;
        }

        private static bool HasLoop(char[][] grid, Coords current, Coords direction)
        {
            var visited = new HashSet<(Coords, Coords)>();
            while (true)
            {
                if (!visited.Add((current, direction)))
                {
                    return true;
                }
                var next = current + direction;
                if (grid.IsOutOfBounds(next))
                {
                    return false;
                }
                if (grid[next.Y][next.X] == '#')
                {
                    direction = direction.RotateRight;
                    continue;
                }
                current = next;
            }
        }

        private static (char[][] Grid, Coords Start) ToGrid(string[] lines)
        {
            var grid = lines.ToCharGrid();
            var start = new Coords(-1, -1);
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == '^')
                    {
                        start = new Coords(x, y);
                        break;
                    }
                }
                if (start.X != -1)
                {
                    break;
                }
            }
            return (grid, start);
        }
    }
}
