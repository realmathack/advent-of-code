namespace AdventOfCode.Y2023.Solvers
{
    public class Day12 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToRecords(input).Sum(record => CountPossibleArrangements(record.Pattern, record.Sizes, []));

        public override object SolvePart2(string[] input) => ToRecords(input, true).Sum(record => CountPossibleArrangements(record.Pattern, record.Sizes, []));

        private static long CountPossibleArrangements(string pattern, int[] sizes, Dictionary<string, long> memo, bool afterGroupEnd = false)
        {
            // https://en.wikipedia.org/wiki/Memoization
            var key = $"{pattern} {string.Join(',', sizes)} {afterGroupEnd}";
            if (memo.TryGetValue(key, out var cachedCount))
            {
                return cachedCount;
            }
            if (pattern.Length == 0L)
            {
                return (sizes.Length != 0) ? 0L : 1L;
            }
            if (sizes.Length == 0)
            {
                return pattern.Any(spring => spring == '#') ? 0L : 1L;
            }
            if (pattern[0] == '.')
            {
                return CountPossibleArrangements(pattern[1..], sizes, memo);
            }
            if (afterGroupEnd)
            {
                return (pattern[0] == '#') ? 0L : CountPossibleArrangements(pattern[1..], sizes, memo);
            }
            var count = 0L;
            if (pattern.Length >= sizes[0] && pattern[..sizes[0]].All(spring => spring != '.'))
            {
                count += CountPossibleArrangements(pattern[sizes[0]..], sizes[1..], memo, true);
            }
            if (pattern[0] != '#') // Does the possibilities part
            {
                count += CountPossibleArrangements(pattern[1..], sizes, memo);
            }
            memo[key] = count;
            return count;
        }

        private static List<(string Pattern, int[] Sizes)> ToRecords(string[] lines, bool unfold = false)
        {
            var records = new List<(string Pattern, int[] Sizes)>();
            foreach (var line in lines)
            {
                var (pattern, sizes) = line.SplitInTwo(' ');
                if (unfold)
                {
                    pattern = string.Join('?', Enumerable.Repeat(pattern, 5));
                    sizes = string.Join(',', Enumerable.Repeat(sizes, 5));
                }
                records.Add((pattern, sizes.Split(',').Select(int.Parse).ToArray()));
            }
            return records;
        }
    }
}
