using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public partial class Day25 : SolverWithText
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

        public override object SolvePart2(string input) => "Last Day";

        private static (long Row, long Col) ToTarget(string message)
        {
            var match = ManualRegex().Match(message);
            return (long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value));
        }

        [GeneratedRegex(@"To continue, please consult the code grid in the manual.  Enter the code at row (\d+), column (\d+)\.")]
        private static partial Regex ManualRegex();
    }
}
