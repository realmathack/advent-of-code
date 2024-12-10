namespace AdventOfCode.Y2024.Solvers
{
    public class Day05 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var (rules, updates) = ToRulesAndUpdates(input);
            var total = 0;
            foreach (var update in updates)
            {
                var indexes = update.Select((value, index) => (value, index)).ToDictionary();
                var ruleSet = rules.Where(rule => update.Contains(rule.Before) || update.Contains(rule.After));
                if (ruleSet.All(rule => !indexes.ContainsKey(rule.Before) || !indexes.ContainsKey(rule.After) || indexes[rule.Before] < indexes[rule.After]))
                {
                    total += update[update.Length / 2];
                }
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var (rules, updates) = ToRulesAndUpdates(input);
            var total = 0;
            foreach (var update in updates)
            {
                var indexes = update.Select((value, index) => (value, index)).ToDictionary();
                var ruleSet = rules.Where(rule => update.Contains(rule.Before) || update.Contains(rule.After));
                if (!ruleSet.All(rule => !indexes.ContainsKey(rule.Before) || !indexes.ContainsKey(rule.After) || indexes[rule.Before] < indexes[rule.After]))
                {
                    var comparer = new PagesComparer(ruleSet.ToHashSet());
                    var ordered = update.Order(comparer).ToArray();
                    total += ordered[ordered.Length / 2];
                }
            }
            return total;

        }

        private static ((int Before, int After)[] Rules, int[][] Updates) ToRulesAndUpdates(string[] lineGroups)
        {
            var rules = lineGroups[0].SplitIntoLines()
                .Select(rule => rule.Split('|', 2))
                .Select(pages => (int.Parse(pages[0]), int.Parse(pages[1])))
                .ToArray();
            var updates = lineGroups[1].SplitIntoLines()
                .Select(update => update.Split(',').Select(int.Parse).ToArray())
                .ToArray();
            return (rules, updates);
        }

        private class PagesComparer(HashSet<(int Before, int After)> rules) : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (rules.Contains((x, y)))
                {
                    return 1;
                }
                if (rules.Contains((y, x)))
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}
