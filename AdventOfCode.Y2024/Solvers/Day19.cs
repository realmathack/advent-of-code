namespace AdventOfCode.Y2024.Solvers
{
    public class Day19 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var towels = input[0].Split(", ");
            var designs = input[1].SplitIntoLines();
            var memo = new Dictionary<string, bool>() { [""] = true };
            return designs.Count(design => HasPossibleSolution(design, towels, memo));
        }

        public override object SolvePart2(string[] input)
        {
            var towels = input[0].Split(", ");
            var designs = input[1].SplitIntoLines();
            var memo = new Dictionary<string, long>() { [""] = 1L };
            return designs.Sum(design => CountPossibleSolutions(design, towels, memo));
        }

        private static bool HasPossibleSolution(string design, string[] towels, Dictionary<string, bool> memo)
        {
            // https://en.wikipedia.org/wiki/Memoization
            if (memo.TryGetValue(design, out var cachedResult))
            {
                return cachedResult;
            }
            foreach (var towel in towels)
            {
                if (towel.Length <= design.Length && design[0..towel.Length] == towel)
                {
                    if (HasPossibleSolution(design[towel.Length..], towels, memo))
                    {
                        memo.TryAdd(design[towel.Length..], true);
                        return true;
                    }
                }
            }
            memo.TryAdd(design, false);
            return false;
        }

        private static long CountPossibleSolutions(string design, string[] towels, Dictionary<string, long> memo)
        {
            // https://en.wikipedia.org/wiki/Memoization
            if (memo.TryGetValue(design, out var cachedResult))
            {
                return cachedResult;
            }
            var count = 0L;
            foreach (var towel in towels)
            {
                if (towel.Length <= design.Length && design[0..towel.Length] == towel)
                {
                    count += CountPossibleSolutions(design[towel.Length..], towels, memo);
                }
            }
            memo.TryAdd(design, count);
            return count;
        }
    }
}
