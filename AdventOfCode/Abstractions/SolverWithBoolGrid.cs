namespace AdventOfCode
{
    public abstract class SolverWithBoolGrid : SolverBase<bool[][]>
    {
        public override bool[][] ParseInput(string input) => ToBoolGrid(input);

        protected static bool[][] ToBoolGrid(string input)
        {
            var lines = input.SplitIntoLines();
            var grid = new bool[lines.Length][];
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                var row = new bool[lines[y].Length];
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (line[x] == '#')
                    {
                        row[x] = true;
                    }
                }
                grid[y] = row;
            }
            return grid;
        }
    }
}
