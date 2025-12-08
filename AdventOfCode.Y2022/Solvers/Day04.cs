using Range = AdventOfCode.Range<int>;

namespace AdventOfCode.Y2022.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToPairs(input).Count(pair => pair.Range1.FullyOverlapsWith(pair.Range2) || pair.Range2.FullyOverlapsWith(pair.Range1));
        public override object SolvePart2(string[] input) => ToPairs(input).Count(pair => pair.Range1.HasAnyOverlapWith(pair.Range2));

        private static List<(Range Range1, Range Range2)> ToPairs(string[] lines)
        {
            var pairs = new List<(Range Range1, Range Range2)>();
            foreach (var line in lines)
            {
                var (assignment1, assignment2) = line.SplitInTwo(',');
                pairs.Add(new(Range.Parse(assignment1), Range.Parse(assignment2)));
            }
            return pairs;
        }
    }
}
