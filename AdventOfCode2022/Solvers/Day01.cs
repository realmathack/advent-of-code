using AdventOfCode2022.Abstractions;

namespace AdventOfCode2022.Solvers
{
    public class Day01 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            return CalculateGroupTotals(input).Max();
        }

        public override object SolvePart2(string[] input)
        {
            return CalculateGroupTotals(input).OrderByDescending(x => x).Take(3).Sum();
        }

        private static List<int> CalculateGroupTotals(string[] sections)
        {
            var totals = new List<int>(sections.Length);
            foreach (var section in sections)
            {
                var total = section.SplitIntoLines().Sum(int.Parse);
                totals.Add(total);
            }
            return totals;
        }
    }
}
