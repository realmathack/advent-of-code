namespace AdventOfCode2022.Solvers
{
    public class Day04 : IBaseSolver
    {
        public string SolvePart1(string input)
        {
            var pairs = ParseInput(input);
            var total = 0;
            foreach (var pair in pairs)
            {
                if (pair.Item1.Count > pair.Item2.Count)
                {
                    if (DoesPairContainsOtherPair(pair.Item1, pair.Item2))
                    {
                        total++;
                    }
                }
                else
                {
                    if (DoesPairContainsOtherPair(pair.Item2, pair.Item1))
                    {
                        total++;
                    }
                }
            }
            return total.ToString();
        }

        public string SolvePart2(string input)
        {
            var pairs = ParseInput(input);
            var total = 0;
            foreach (var pair in pairs)
            {
                if (DoesPairOverlapWithOtherPair(pair.Item1, pair.Item2))
                {
                    total++;
                }
            }
            return total.ToString();
        }

        private static List<(List<int>, List<int>)> ParseInput(string input)
        {
            var result = new List<(List<int>, List<int>)>();
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var assignments = line.Split(',');
                var assignment1 = GetSections(assignments[0]);
                var assignment2 = GetSections(assignments[1]);
                result.Add((assignment1, assignment2));
            }
            return result;
        }

        private static List<int> GetSections(string sectionBounds)
        {
            var bounds = sectionBounds.Split('-');
            var lower = int.Parse(bounds[0]);
            var upper = int.Parse(bounds[1]);
            return Enumerable.Range(lower, upper - lower + 1).ToList();
        }

        private static bool DoesPairContainsOtherPair(List<int> biggerPair, List<int> smallerPair)
        {
            return biggerPair.Contains(smallerPair.Min()) && biggerPair.Contains(smallerPair.Max());
        }

        private static bool DoesPairOverlapWithOtherPair(List<int> firstPair, List<int> secondPair)
        {
            return firstPair.Intersect(secondPair).Any();
        }
    }
}
