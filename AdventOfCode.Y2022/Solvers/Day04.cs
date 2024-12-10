using Range = AdventOfCode.Range<int>;

namespace AdventOfCode.Y2022.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToPairs(input).Count(pair => pair.Range1.FullyOverlapsWith(pair.Range2) || pair.Range2.FullyOverlapsWith(pair.Range1));
        public override object SolvePart2(string[] input) => ToPairs(input).Count(pair => pair.Range1.HasAnyOverlapWith(pair.Range2));

        private static List<Pair> ToPairs(string[] lines)
        {
            var pairs = new List<Pair>();
            foreach (var line in lines)
            {
                var (assignment1, assignment2) = line.SplitInTwo(',');
                pairs.Add(new(ToRange(assignment1), ToRange(assignment2)));
            }
            return pairs;
        }

        private static Range ToRange(string assignment)
        {
            var (start, end) = assignment.SplitInTwo('-');
            return new(int.Parse(start), int.Parse(end));
        }

        private record class Pair(Range Range1, Range Range2);
    }
}
