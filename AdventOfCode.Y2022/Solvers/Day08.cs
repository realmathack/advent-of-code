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
                    if (IsVisible(FromLeft(grid, row, col)) || IsVisible(FromRight(grid, row, col)) ||
                        IsVisible(FromTop(grid, row, col)) || IsVisible(FromBottom(grid, row, col)))
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
                    var currentView = CalculateDistance(FromLeft(grid, row, col)) * CalculateDistance(FromRight(grid, row, col)) *
                        CalculateDistance(FromTop(grid, row, col)) * CalculateDistance(FromBottom(grid, row, col));
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

        private static bool IsVisible(int[] viewLine)
        {
            return viewLine[^1] == viewLine.Max() && viewLine.Count(x => x == viewLine[^1]) == 1;
        }

        private static int CalculateDistance(int[] viewLine)
        {
            if (viewLine.Length == 1)
            {
                return 0;
            }
            for (int i = viewLine.Length - 2; i >= 0; i--)
            {
                if (viewLine[i] >= viewLine[^1])
                {
                    return viewLine.Length - i - 1;
                }
            }
            return viewLine.Length - 1;
        }

        private static int[] FromLeft(int[][] grid, int row, int col)
        {
            return grid[row][0..(col + 1)];
        }

        private static int[] FromRight(int[][] grid, int row, int col)
        {
            var result = grid[row][col..^0];
            Array.Reverse(result);
            return result;
        }

        private static int[] FromTop(int[][] grid, int row, int col)
        {
            var result = new int[row + 1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = grid[i][col];
            }
            return result;
        }

        private static int[] FromBottom(int[][] grid, int row, int col)
        {
            var result = new int[grid.Length - row];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = grid[^(i + 1)][col];
            }
            return result;
        }
    }
}
