namespace AdventOfCode.Y2015.Solvers
{
    public class Day18 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => SolvePart1(input, 100);
        public int SolvePart1(string[] input, int steps)
        {
            var grid = ToGrid(input);
            for (int step = 0; step < steps; step++)
            {
                grid = DoStep(grid);
            }
            return grid.Select(row => row.Count(light => light)).Sum();
        }

        public override object SolvePart2(string[] input) => SolvePart2(input, 100);
        public int SolvePart2(string[] input, int steps)
        {
            var grid = SetCornersOn(ToGrid(input));
            for (int step = 0; step < steps; step++)
            {
                grid = SetCornersOn(DoStep(grid));
            }
            return grid.Select(row => row.Count(light => light)).Sum();
        }

        private static bool[][] DoStep(bool[][] grid)
        {
            var newGrid = new bool[grid.Length][];
            for (int row = 0; row < grid.Length; row++)
            {
                newGrid[row] = new bool[grid[row].Length];
                for (int col = 0; col < grid[row].Length; col++)
                {
                    newGrid[row][col] = CalculateNewState(grid, row, col);
                }
            }
            return newGrid;
        }

        private static bool[][] ToGrid(string[] lines)
        {
            var grid = new bool[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = lines[i].Select(light => light == '#').ToArray();
            }
            return grid;
        }

        private static bool CalculateNewState(bool[][] grid, int row, int col)
        {
            var neighborsLit = 0;
            if (row > 0 &&                  col > 0 &&                      grid[row - 1][col - 1]) { neighborsLit++; } // TL
            if (row > 0 &&                                                  grid[row - 1][col])     { neighborsLit++; } // T
            if (row > 0 &&                  col < grid[row].Length - 1 &&   grid[row - 1][col + 1]) { neighborsLit++; } // TR
            if (                            col > 0 &&                      grid[row][col - 1])     { neighborsLit++; } //  L
            if (                            col < grid[row].Length - 1 &&   grid[row][col + 1])     { neighborsLit++; } //  R
            if (row < grid.Length - 1 &&    col > 0 &&                      grid[row + 1][col - 1]) { neighborsLit++; } // BL
            if (row < grid.Length - 1 &&                                    grid[row + 1][col])     { neighborsLit++; } // B
            if (row < grid.Length - 1 &&    col < grid[row].Length - 1 &&   grid[row + 1][col + 1]) { neighborsLit++; } // BR
            return grid[row][col] ? (neighborsLit == 2 || neighborsLit == 3) : (neighborsLit == 3);
        }

        private static bool[][] SetCornersOn(bool[][] grid)
        {
            grid[0][0] = true;
            grid[0][^1] = true;
            grid[^1][0] = true;
            grid[^1][^1] = true;
            return grid;
        }
    }
}
