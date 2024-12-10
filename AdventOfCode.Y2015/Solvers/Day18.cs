namespace AdventOfCode.Y2015.Solvers
{
    public class Day18(int _steps) : SolverWithBoolGrid
    {
        public Day18() : this(100) { }

        public override object SolvePart1(bool[][] grid)
        {
            for (int step = 0; step < _steps; step++)
            {
                grid = ExecuteStep(grid);
            }
            return grid.Select(row => row.Count(light => light)).Sum();
        }

        public override object SolvePart2(bool[][] grid)
        {
            SetCornersOn(grid);
            for (int step = 0; step < _steps; step++)
            {
                grid = SetCornersOn(ExecuteStep(grid));
            }
            return grid.Select(row => row.Count(light => light)).Sum();
        }

        private static bool[][] ExecuteStep(bool[][] grid)
        {
            var newGrid = new bool[grid.Length][];
            for (int y = 0; y < grid.Length; y++)
            {
                newGrid[y] = new bool[grid[y].Length];
                for (int x = 0; x < grid[y].Length; x++)
                {
                    newGrid[y][x] = CalculateNewState(grid, y, x);
                }
            }
            return newGrid;
        }

        private static bool CalculateNewState(bool[][] grid, int y, int x)
        {
            var neighborsLit = 0;
            if (y > 0 &&                  x > 0 &&                    grid[y - 1][x - 1]) { neighborsLit++; } // TL
            if (y > 0 &&                                              grid[y - 1][x])     { neighborsLit++; } // T
            if (y > 0 &&                  x < grid[y].Length - 1 &&   grid[y - 1][x + 1]) { neighborsLit++; } // TR
            if (                          x > 0 &&                    grid[y][x - 1])     { neighborsLit++; } //  L
            if (                          x < grid[y].Length - 1 &&   grid[y][x + 1])     { neighborsLit++; } //  R
            if (y < grid.Length - 1 &&    x > 0 &&                    grid[y + 1][x - 1]) { neighborsLit++; } // BL
            if (y < grid.Length - 1 &&                                grid[y + 1][x])     { neighborsLit++; } // B
            if (y < grid.Length - 1 &&    x < grid[y].Length - 1 &&   grid[y + 1][x + 1]) { neighborsLit++; } // BR
            return grid[y][x] ? (neighborsLit == 2 || neighborsLit == 3) : (neighborsLit == 3);
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
