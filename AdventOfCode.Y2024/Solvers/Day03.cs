using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Solvers
{
    public partial class Day03 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var total = 0;
            foreach (var match in (IReadOnlyList<Match>)Regex1().Matches(input))
            {
                total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
            return total;
        }

        public override object SolvePart2(string input)
        {
            var total = 0;
            var enabled = true;
            foreach (var match in (IReadOnlyList<Match>)Regex2().Matches(input))
            {
                if (match.Groups[0].Value == "do()")
                {
                    enabled = true;
                }
                else if (match.Groups[0].Value == "don't()")
                {
                    enabled = false;
                }
                else if (enabled)
                {
                    total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }
            }
            return total;
        }

        [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
        private static partial Regex Regex1();
        [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)")]
        private static partial Regex Regex2();
    }
}
