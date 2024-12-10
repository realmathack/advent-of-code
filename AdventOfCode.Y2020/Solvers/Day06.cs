namespace AdventOfCode.Y2020.Solvers
{
    public class Day06 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input) => input.Sum(lineGroup => lineGroup.SplitIntoLines().SelectMany(answers => answers.ToCharArray()).Distinct().Count());

        public override object SolvePart2(string[] input)
        {
            var sum = 0;
            foreach (var lineGroup in input)
            {
                var lines = lineGroup.SplitIntoLines();
                sum += lines
                    .SelectMany(answers => answers.ToCharArray())
                    .GroupBy(answer => answer)
                    .Select(g => (g.Key, Count: g.Count()))
                    .ToDictionary()
                    .Count(count => count.Value == lines.Length);
            }
            return sum;
        }
    }
}
