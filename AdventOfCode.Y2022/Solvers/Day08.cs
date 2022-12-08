namespace AdventOfCode.Y2022.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var dimensions = GetDimensions(input);
            var grid = ToGrid(input, dimensions);
            var results = new HashSet<int>();
            for (int i = 0; i < grid.Length; i++)
            {
                if (IsEdge(i, dimensions) ||
                    IsVisible(FromLeft(i, grid, dimensions)) ||
                    IsVisible(FromRight(i, grid, dimensions)) ||
                    IsVisible(FromTop(i, grid, dimensions)) ||
                    IsVisible(FromBottom(i, grid, dimensions)))
                {
                    results.Add(i);
                    continue;
                }
            }
            return results.Count;
        }

        public override object SolvePart2(string[] input)
        {
            var dimensions = GetDimensions(input);
            var grid = ToGrid(input, dimensions);
            var result = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                var currentView = CalculateDistance(FromLeft(i, grid, dimensions)) * CalculateDistance(FromRight(i, grid, dimensions)) *
                    CalculateDistance(FromTop(i, grid, dimensions)) * CalculateDistance(FromBottom(i, grid, dimensions));
                if (currentView > result)
                {
                    result = currentView;
                }
            }
            return result;
        }

        private static Dimensions GetDimensions(string[] lines) => new(lines.Length, lines[0].Length);

        private static int[] ToGrid(string[] lines, Dimensions dimensions)
        {
            var result = new int[dimensions.Height * dimensions.Width];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = lines[i / dimensions.Width][i % dimensions.Width] - '0';
            }
            return result;
        }

        private static bool IsEdge(int tree, Dimensions dimensions)
        {
            return (tree % dimensions.Width == 0 || tree / dimensions.Width == 0 || (tree + 1) % dimensions.Width == 0 || (tree + 1) / dimensions.Width == 0);
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

        private static int[] FromLeft(int tree, int[] grid, Dimensions dimensions)
        {
            var edge = tree - tree % dimensions.Width;
            return grid[edge..(tree + 1)];
        }

        private static int[] FromRight(int tree, int[] grid, Dimensions dimensions)
        {
            var nextRow = (tree / dimensions.Width + 1) * dimensions.Width;
            return grid[tree..nextRow].Reverse().ToArray();
        }

        private static int[] FromTop(int tree, int[] grid, Dimensions dimensions)
        {
            var result = new int[tree / dimensions.Width + 1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = grid[i * dimensions.Width + tree % dimensions.Width];
            }
            return result;
        }

        private static int[] FromBottom(int tree, int[] grid, Dimensions dimensions)
        {
            var result = new int[dimensions.Height - tree / dimensions.Width];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = grid[(dimensions.Height - i - 1) * dimensions.Width + tree % dimensions.Width];
            }
            return result;
        }

        private record struct Dimensions(int Height, int Width);
    }
}
