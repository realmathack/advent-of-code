namespace AdventOfCode.Y2022.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var grid = ToGrid(input);
            var count = (grid.Length + grid[0].Length - 2) * 2;
            for (int row = 1; row < grid.Length - 1; row++)
            {
                for (int col = 1; col < grid[row].Length - 1; col++)
                {
                    if (IsVisibleFromLeft(grid, row, col) || IsVisibleFromRight(grid, row, col) ||
                        IsVisibleFromTop(grid, row, col) || IsVisibleFromBottom(grid, row, col))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public override object SolvePart2(string[] input)
        {
            var grid = ToGrid(input);
            var biggestView = 0;
            for (int row = 1; row < grid.Length - 1; row++)
            {
                for (int col = 1; col < grid[row].Length - 1; col++)
                {
                    var currentView = CalculateDistanceFromLeft(grid, row, col) * CalculateDistanceFromRight(grid, row, col) *
                        CalculateDistanceFromTop(grid, row, col) * CalculateDistanceFromBottom(grid, row, col);
                    if (currentView > biggestView)
                    {
                        biggestView = currentView;
                    }
                }
            }
            return biggestView;
        }

        private static int[][] ToGrid(string[] lines)
        {
            var result = new int[lines.Length][];
            for (int row = 0; row < lines.Length; row++)
            {
                result[row] = new int[lines[row].Length];
                for (int col = 0; col < lines[row].Length; col++)
                {
                    result[row][col] = lines[row][col] - '0';
                }
            }
            return result;
        }

        private static bool IsVisibleFromLeft(int[][] grid, int row, int col)
        {
            for (int i = col - 1; i >= 0; i--)
            {
                if (grid[row][i] >= grid[row][col])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleFromRight(int[][] grid, int row, int col)
        {
            for (int i = col + 1; i < grid[row].Length; i++)
            {
                if (grid[row][i] >= grid[row][col])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleFromTop(int[][] grid, int row, int col)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (grid[i][col] >= grid[row][col])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleFromBottom(int[][] grid, int row, int col)
        {
            for (int i = row + 1; i < grid.Length; i++)
            {
                if (grid[i][col] >= grid[row][col])
                {
                    return false;
                }
            }
            return true;
        }

        private static int CalculateDistanceFromLeft(int[][] grid, int row, int col)
        {
            for (int i = col - 1; i >= 0; i--)
            {
                if (grid[row][i] >= grid[row][col])
                {
                    return col - i;
                }
            }
            return col;
        }

        private static int CalculateDistanceFromRight(int[][] grid, int row, int col)
        {
            for (int i = col + 1; i < grid[row].Length; i++)
            {
                if (grid[row][i] >= grid[row][col])
                {
                    return i - col;
                }
            }
            return grid[row].Length - 1 - col;
        }

        private static int CalculateDistanceFromTop(int[][] grid, int row, int col)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (grid[i][col] >= grid[row][col])
                {
                    return row - i;
                }
            }
            return row;
        }

        private static int CalculateDistanceFromBottom(int[][] grid, int row, int col)
        {
            for (int i = row + 1; i < grid.Length; i++)
            {
                if (grid[i][col] >= grid[row][col])
                {
                    return i - row;
                }
            }
            return grid.Length - 1 - row;
        }
    }
}
