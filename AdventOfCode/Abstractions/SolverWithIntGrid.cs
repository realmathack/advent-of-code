namespace AdventOfCode
{
    public abstract class SolverWithIntGrid : SolverBase<int[][]>
    {
        public override int[][] ParseInput(string input) => ToIntGrid(input);

        protected static int[][] ToIntGrid(string input)
        {
            var lines = input.SplitIntoLines();
            var grid = new int[lines.Length][];
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                var row = new int[lines[y].Length];
                for (int x = 0; x < lines[y].Length; x++)
                {
                    row[x] = line[x] - '0';
                }
                grid[y] = row;
            }
            return grid;
        }
    }
}
