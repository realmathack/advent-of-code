namespace AdventOfCode.Y2015.Solvers
{
    public class Day24 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => FindIdealConfiguration(input, 3);

        public override object SolvePart2(string[] input) => FindIdealConfiguration(input, 4);

        private static long FindIdealConfiguration(string[] lines, int compartments)
        {
            var packages = lines.Select(int.Parse).ToList();
            var targetWeight = packages.Sum() / compartments;
            for (int depth = 0; depth < packages.Count; depth++)
            {
                var sets = ToPackageSets(packages, 0, depth, targetWeight);
                if (sets.Any())
                {
                    return sets.Min(CalculateQuantumEntanglement);
                }
            }
            return 0;
        }

        private static IEnumerable<HashSet<int>> ToPackageSets(List<int> packages, int i, int depthLeft, int remainder)
        {
            if (remainder == 0)
            {
                yield return new HashSet<int>();
                yield break;
            }
            if (remainder < 0 || depthLeft < 0 || i >= packages.Count)
            {
                yield break;
            }
            if (packages[i] <= remainder)
            {
                foreach (var set in ToPackageSets(packages, i + 1, depthLeft - 1, remainder - packages[i]))
                {
                    set.Add(packages[i]);
                    yield return set;
                }
            }
            foreach (var set in ToPackageSets(packages, i + 1, depthLeft, remainder))
            {
                yield return set;
            }
        }

        private static long CalculateQuantumEntanglement(HashSet<int> set) => set.Aggregate(1L, (quantumEntanglement, package) => quantumEntanglement * package);
    }
}
