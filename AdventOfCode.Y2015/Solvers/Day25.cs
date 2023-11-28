namespace AdventOfCode.Y2015.Solvers
{
    public class Day25 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var (row, col) = ToTarget(input);
            var codeNumber = ((1 + col) * col) / 2; // col-based (eg. 1+2+3 for col 3)
            codeNumber += ((col * 2 + (row - 2)) * (row - 1)) / 2; // row-based (eg. 3+4+5 for row 4)
            var code = 20151125L;
            for (int i = 1; i < codeNumber; i++)
            {
                code = (code * 252533) % 33554393;
            }
            return code;
        }

        public override object SolvePart2(string input) => "Day 25";

        private static readonly char[] _separator = [' ', ',', '.'];
        private static (long row, long col) ToTarget(string input)
        {
            var parts = input.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
            return (long.Parse(parts[15]), long.Parse(parts[17]));
        }
    }
}
