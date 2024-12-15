namespace AdventOfCode.Y2024.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (leftIds, rightIds) = ToLocations(input);
            var total = 0;
            for (int i = 0; i < leftIds.Count; i++)
            {
                total += Math.Abs(leftIds[i] - rightIds[i]);
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var (leftIds, rightIds) = ToLocations(input);
            var scores = new Dictionary<int, int>(leftIds.Count);
            var total = 0;
            foreach (var leftId in leftIds)
            {
                if (!scores.TryGetValue(leftId, out var score))
                {
                    score = rightIds.Count(rightId => rightId == leftId) * leftId;
                    scores[leftId] = score;
                }
                total += score;
            }
            return total;
        }

        private static (List<int> Left, List<int> Right) ToLocations(string[] input)
        {
            var leftIds = new List<int>(input.Length);
            var rightIds = new List<int>(input.Length);
            foreach (var line in input)
            {
                var (left, right) = line.SplitInTwo("   ");
                leftIds.Add(int.Parse(left));
                rightIds.Add(int.Parse(right));
            }
            leftIds.Sort();
            rightIds.Sort();
            return (leftIds, rightIds);
        }
    }
}
