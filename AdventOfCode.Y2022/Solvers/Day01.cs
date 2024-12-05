namespace AdventOfCode.Y2022.Solvers
{
    public class Day01 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input) => CalculateGroupTotals(input).Max();
        public override object SolvePart2(string[] input) => CalculateGroupTotals(input).OrderByDescending(total => total).Take(3).Sum();

        private static List<int> CalculateGroupTotals(string[] lineGroups)
        {
            var totals = new List<int>(lineGroups.Length);
            foreach (var lineGroup in lineGroups)
            {
                var total = lineGroup.SplitIntoLines().Sum(int.Parse);
                totals.Add(total);
            }
            return totals;
        }
    }
}
