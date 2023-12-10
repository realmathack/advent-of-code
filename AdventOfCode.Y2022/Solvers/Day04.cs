namespace AdventOfCode.Y2022.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToPairs(input).Count(pair => pair.Range1.FullyOverlaps(pair.Range2) || pair.Range2.FullyOverlaps(pair.Range1));
        public override object SolvePart2(string[] input) => ToPairs(input).Count(pair => pair.Range1.AnyOverlap(pair.Range2));

        private static List<Pair> ToPairs(string[] lines)
        {
            var result = new List<Pair>();
            foreach (var line in lines)
            {
                var assignments = line.Split(',');
                result.Add(new(ToRange(assignments[0]), ToRange(assignments[1])));
            }
            return result;
        }

        private static Range<int> ToRange(string section)
        {
            var bounds = section.Split('-');
            return new(int.Parse(bounds[0]), int.Parse(bounds[1]));
        }

        private record class Pair(Range<int> Range1, Range<int> Range2);
    }
}
