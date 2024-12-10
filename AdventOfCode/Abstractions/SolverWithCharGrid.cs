namespace AdventOfCode
{
    public abstract class SolverWithCharGrid : SolverBase<char[][]>
    {
        public override char[][] ParseInput(string input) => ToCharGrid(input);

        protected static char[][] ToCharGrid(string input)
        {
            var lines = input.SplitIntoLines();
            var grid = new char[lines.Length][];
            for (int y = 0; y < lines.Length; y++)
            {
                grid[y] = lines[y].ToCharArray();
            }
            return grid;
        }
    }
}
