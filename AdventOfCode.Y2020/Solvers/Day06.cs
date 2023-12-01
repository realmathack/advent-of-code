namespace AdventOfCode.Y2020.Solvers
{
    public class Day06 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            return input.Sum(x => x.SplitIntoLines().SelectMany(x => x.ToCharArray()).Distinct().Count());
        }

        public override object SolvePart2(string[] input)
        {
            var sum = 0;
            foreach (var section in input)
            {
                var lines = section.SplitIntoLines();
                sum += lines
                    .SelectMany(x => x.ToCharArray())
                    .GroupBy(x => x)
                    .Select(g => new { g.Key, Count = g.Count() })
                    .ToDictionary(x => x.Key, x => x.Count)
                    .Count(x => x.Value == lines.Length);
            }
            return sum;
        }
    }
}
