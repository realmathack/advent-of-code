namespace AdventOfCode.Y2022.Solvers
{
    public class Day08 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            var count = (grid.Length + grid[0].Length - 2) * 2;
            for (int y = 1; y < grid.Length - 1; y++)
            {
                for (int x = 1; x < grid[y].Length - 1; x++)
                {
                    if (IsVisibleFromLeft(grid, y, x) || IsVisibleFromRight(grid, y, x) ||
                        IsVisibleFromTop(grid, y, x) || IsVisibleFromBottom(grid, y, x))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public override object SolvePart2(char[][] grid)
        {
            var biggestView = 0;
            for (int y = 1; y < grid.Length - 1; y++)
            {
                for (int x = 1; x < grid[y].Length - 1; x++)
                {
                    var currentView = CalculateDistanceFromLeft(grid, y, x) * CalculateDistanceFromRight(grid, y, x) *
                        CalculateDistanceFromTop(grid, y, x) * CalculateDistanceFromBottom(grid, y, x);
                    if (currentView > biggestView)
                    {
                        biggestView = currentView;
                    }
                }
            }
            return biggestView;
        }

        private static bool IsVisibleFromLeft(char[][] grid, int y, int x)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (grid[y][i] >= grid[y][x])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleFromRight(char[][] grid, int y, int x)
        {
            for (int i = x + 1; i < grid[y].Length; i++)
            {
                if (grid[y][i] >= grid[y][x])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleFromTop(char[][] grid, int y, int x)
        {
            for (int i = y - 1; i >= 0; i--)
            {
                if (grid[i][x] >= grid[y][x])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleFromBottom(char[][] grid, int y, int x)
        {
            for (int i = y + 1; i < grid.Length; i++)
            {
                if (grid[i][x] >= grid[y][x])
                {
                    return false;
                }
            }
            return true;
        }

        private static int CalculateDistanceFromLeft(char[][] grid, int y, int x)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (grid[y][i] >= grid[y][x])
                {
                    return x - i;
                }
            }
            return x;
        }

        private static int CalculateDistanceFromRight(char[][] grid, int y, int x)
        {
            for (int i = x + 1; i < grid[y].Length; i++)
            {
                if (grid[y][i] >= grid[y][x])
                {
                    return i - x;
                }
            }
            return grid[y].Length - 1 - x;
        }

        private static int CalculateDistanceFromTop(char[][] grid, int y, int x)
        {
            for (int i = y - 1; i >= 0; i--)
            {
                if (grid[i][x] >= grid[y][x])
                {
                    return y - i;
                }
            }
            return y;
        }

        private static int CalculateDistanceFromBottom(char[][] grid, int y, int x)
        {
            for (int i = y + 1; i < grid.Length; i++)
            {
                if (grid[i][x] >= grid[y][x])
                {
                    return i - y;
                }
            }
            return grid.Length - 1 - y;
        }
    }
}
