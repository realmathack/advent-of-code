namespace AdventOfCode.Y2024.Solvers
{
    public class Day04 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            var count = 0;
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == 'X')
                    {
                        count += CountDirections(grid, new(x, y));
                    }
                }
            }
            return count;
        }

        public override object SolvePart2(char[][] grid)
        {
            var count = 0;
            for (int y = 1; y < grid.Length - 1; y++)
            {
                for (int x = 1; x < grid[y].Length - 1; x++)
                {
                    if (grid[y][x] == 'A' && HasXWords(grid, new(x, y)))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static int CountDirections(char[][] grid, Coords start)
        {
            var count = 0;
            foreach (var offset in Coords.AdjacentOffsets)
            {
                if (HasWordInDirection(grid, start, offset, 0))
                {
                    count++;
                }
            }
            return count;
        }

        private static readonly string _word = "MAS";
        private static bool HasWordInDirection(char[][] grid, Coords current, Coords offset, int index)
        {
            if (index >= _word.Length)
            {
                return true;
            }
            current += offset;
            if (grid.IsOutOfBounds(current) || grid[current.Y][current.X] != _word[index])
            {
                return false;
            }
            return HasWordInDirection(grid, current, offset, index + 1);
        }

        private static bool HasXWords(char[][] grid, Coords start)
        {
            if (!(grid[start.Y - 1][start.X - 1] == 'M' && grid[start.Y + 1][start.X + 1] == 'S'
               || grid[start.Y - 1][start.X - 1] == 'S' && grid[start.Y + 1][start.X + 1] == 'M'))
            {
                return false;
            }
            if (!(grid[start.Y - 1][start.X + 1] == 'M' && grid[start.Y + 1][start.X - 1] == 'S'
               || grid[start.Y - 1][start.X + 1] == 'S' && grid[start.Y + 1][start.X - 1] == 'M'))
            {
                return false;
            }
            return true;
        }
    }
}
