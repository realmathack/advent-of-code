namespace AdventOfCode.Y2024.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (left, right) = ToLocations(input);
            var total = 0;
            for (int i = 0; i < left.Count; i++)
            {
                total += Math.Abs(left[i] - right[i]);
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var (left, right) = ToLocations(input);
            var scores = new Dictionary<int, int>(left.Count);
            var total = 0;
            foreach (var leftId in left)
            {
                if (!scores.TryGetValue(leftId, out var score))
                {
                    score = right.Count(rightId => rightId == leftId) * leftId;
                    scores[leftId] = score;
                }
                total += score;
            }
            return total;
        }

        private static (List<int> Left, List<int> Right) ToLocations(string[] input)
        {
            var left = new List<int>(input.Length);
            var right = new List<int>(input.Length);
            foreach (var line in input)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                left.Add(int.Parse(parts[0]));
                right.Add(int.Parse(parts[1]));
            }
            left.Sort();
            right.Sort();
            return (left, right);
        }
    }
}
