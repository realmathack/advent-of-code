namespace AdventOfCode.Y2016.Solvers
{
    public class Day06 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            var message = string.Empty;
            for (int x = 0; x < grid[0].Length; x++)
            {
                message += grid.Select(row => row[x]).GroupBy(letter => letter).OrderByDescending(g => g.Count()).First().Key;
            }
            return message;
        }

        public override object SolvePart2(char[][] grid)
        {
            var message = string.Empty;
            for (int x = 0; x < grid[0].Length; x++)
            {
                message += grid.Select(row => row[x]).GroupBy(letter => letter).OrderByDescending(g => g.Count()).Last().Key;
            }
            return message;
        }
    }
}
