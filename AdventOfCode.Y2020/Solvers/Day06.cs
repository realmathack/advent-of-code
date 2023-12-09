namespace AdventOfCode.Y2020.Solvers
{
    public class Day06 : SolverWithSections
    {
        public override object SolvePart1(string[] input) => input.Sum(section => section.SplitIntoLines().SelectMany(answers => answers.ToCharArray()).Distinct().Count());

        public override object SolvePart2(string[] input)
        {
            var sum = 0;
            foreach (var section in input)
            {
                var lines = section.SplitIntoLines();
                sum += lines
                    .SelectMany(answers => answers.ToCharArray())
                    .GroupBy(answer => answer)
                    .Select(g => (g.Key, Count: g.Count()))
                    .ToDictionary(g => g.Key, g => g.Count)
                    .Count(count => count.Value == lines.Length);
            }
            return sum;
        }
    }
}
