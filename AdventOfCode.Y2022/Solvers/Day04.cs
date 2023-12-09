namespace AdventOfCode.Y2022.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => GetPairs(input).Count(pair => FullOverlap(pair.Pair1, pair.Pair2));

        public override object SolvePart2(string[] input) => GetPairs(input).Count(pair => AnyOverlap(pair.Pair1, pair.Pair2));

        private static List<(Pair Pair1, Pair Pair2)> GetPairs(string[] lines)
        {
            var result = new List<(Pair Pair1, Pair Pair2)>();
            foreach (var line in lines)
            {
                var assignments = line.Split(',');
                result.Add((GetSections(assignments[0]), GetSections(assignments[1])));
            }
            return result;
        }

        private static Pair GetSections(string assignment)
        {
            var bounds = assignment.Split('-');
            return new(int.Parse(bounds[0]), int.Parse(bounds[1]));
        }

        private static bool FullOverlap(Pair pair1, Pair pair2)
        {
            return (pair1.Lower >= pair2.Lower && pair2.Upper >= pair1.Upper) || (pair2.Lower >= pair1.Lower && pair1.Upper >= pair2.Upper);
        }

        private static bool AnyOverlap(Pair pair1, Pair pair2)
        {
            return pair1.Lower <= pair2.Upper && pair2.Lower <= pair1.Upper;
        }

        private record struct Pair(int Lower, int Upper);
    }
}
