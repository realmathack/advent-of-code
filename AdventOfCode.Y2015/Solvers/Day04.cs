namespace AdventOfCode.Y2015.Solvers
{
    public class Day04 : SolverWithText
    {
        public override object SolvePart1(string input) => FindFirstHashStartingWith(input, "00000");

        public override object SolvePart2(string input) => FindFirstHashStartingWith(input, "000000");

        private static int FindFirstHashStartingWith(string input, string start)
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                if ((input + i).ToMD5Hex().StartsWith(start))
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
