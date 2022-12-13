namespace AdventOfCode.Y2016.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var message = "";
            var grid = ToGrid(input);
            for (int col = 0; col < grid[0].Length; col++)
            {
                message += grid.Select(row => row[col]).GroupBy(letter => letter).OrderByDescending(g => g.Count()).First().Key;
            }
            return message;
        }

        public override object SolvePart2(string[] input)
        {
            var message = "";
            var grid = ToGrid(input);
            for (int col = 0; col < grid[0].Length; col++)
            {
                message += grid.Select(row => row[col]).GroupBy(letter => letter).OrderByDescending(g => g.Count()).Last().Key;
            }
            return message;
        }

        private static char[][] ToGrid(string[] lines)
        {
            var grid = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }
            return grid;
        }
    }
}
