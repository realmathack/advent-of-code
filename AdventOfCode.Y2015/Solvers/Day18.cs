namespace AdventOfCode.Y2015.Solvers
{
    public class Day18(int steps) : SolverWithLines
    {
        public Day18() : this(100) { }

        public override object SolvePart1(string[] input)
        {
            var grid = ToGrid(input);
            for (int step = 0; step < steps; step++)
            {
                grid = ExecuteStep(grid);
            }
            return grid.Select(row => row.Count(light => light)).Sum();
        }

        public override object SolvePart2(string[] input)
        {
            var grid = SetCornersOn(ToGrid(input));
            for (int step = 0; step < steps; step++)
            {
                grid = SetCornersOn(ExecuteStep(grid));
            }
            return grid.Select(row => row.Count(light => light)).Sum();
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

        private static bool[][] ExecuteStep(bool[][] grid)
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
