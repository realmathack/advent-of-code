using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public partial class Day12 : SolverWithText
    {
        public override object SolvePart1(string input) => SumNumbers(input);

        public override object SolvePart2(string input)
        {
            int pos;
            while ((pos = input.IndexOf(":\"red\"")) > -1)
            {
                var depth = -1;
                var start = pos;
                while (depth < 0)
                {
                    var token = input[--start];
                    if (token == '{') { depth++; continue; }
                    if (token == '}') { depth--; continue; }
                }
                depth = 1;
                while (depth > 0)
                {
                    var token = input[pos++];
                    if (token == '{') { depth++; continue; }
                    if (token == '}') { depth--; continue; }
                }
                input = input[..start] + input[pos..];
            }
            return SumNumbers(input);
        }

        private static int SumNumbers(string numbers)
        {
            var sum = 0;
            foreach (var match in (IReadOnlyList<Match>)NumberRegex().Matches(numbers))
            {
                sum += int.Parse(match.Groups[0].Value);
            }
            return sum;
        }

        [GeneratedRegex(@"-?\d+")]
        private static partial Regex NumberRegex();
    }
}
