namespace AdventOfCode.Y2022.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToPairs(input).Count(pair => FullOverlap(pair.Range1, pair.Range2));

        public override object SolvePart2(string[] input) => ToPairs(input).Count(pair => AnyOverlap(pair.Range1, pair.Range2));

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

        private static Int32Range ToRange(string section)
        {
            var bounds = section.Split('-');
            return new(int.Parse(bounds[0]), int.Parse(bounds[1]));
        }

        private static bool FullOverlap(Int32Range a, Int32Range b)
        {
            return (a.Start >= b.Start && b.End >= a.End) || (b.Start >= a.Start && a.End >= b.End);
        }

        private static bool AnyOverlap(Int32Range a, Int32Range b)
        {
            return a.Start <= b.End && b.Start <= a.End;
        }

        private record class Pair(Int32Range Range1, Int32Range Range2);
        private readonly record struct Int32Range(int Start, int End);
    }
}
