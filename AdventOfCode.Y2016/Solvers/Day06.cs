namespace AdventOfCode.Y2016.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var message = string.Empty;
            var grid = input.ToCharGrid();
            for (int col = 0; col < grid[0].Length; col++)
            {
                message += grid.Select(row => row[col]).GroupBy(letter => letter).OrderByDescending(g => g.Count()).First().Key;
            }
            return message;
        }

        public override object SolvePart2(string[] input)
        {
            var message = string.Empty;
            var grid = input.ToCharGrid();
            for (int col = 0; col < grid[0].Length; col++)
            {
                message += grid.Select(row => row[col]).GroupBy(letter => letter).OrderByDescending(g => g.Count()).Last().Key;
            }
            return message;
        }
    }
}
