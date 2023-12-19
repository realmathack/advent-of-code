namespace AdventOfCode.Y2017.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var programs = new HashSet<string>();
            var childeren = new HashSet<string>();
            foreach (var line in input)
            {
                var parts = line.Split(" -> ");
                if (parts.Length > 1)
                {
                    childeren.UnionWith(parts[1].Split(", "));
                }
                programs.Add(parts[0].Split(' ')[0]);
            }
            return programs.Except(childeren).Single();
        }

        public override object SolvePart2(string[] input)
        {
            var programs = new Dictionary<string, (int Weight, List<string> Children)>();
            var alllChilderen = new HashSet<string>();
            foreach (var line in input)
            {
                var parts = line.Split(" -> ");
                var children = (parts.Length == 1) ? [] : parts[1].Split(", ").ToList();
                alllChilderen.UnionWith(children);
                parts = parts[0].Split(' ');
                programs.Add(parts[0], (int.Parse(parts[1].Trim('(', ')')), children));
            }
            var root = programs.Keys.ToHashSet().Except(alllChilderen).Single();
            var (found, correctedWeight, _) = CheckWeights(programs, root);
            if (!found)
            {
                throw new InvalidOperationException("Solution not found");
            }
            return correctedWeight;
        }

        private static (bool Found, int CorrectedWeight, int SummedWeight) CheckWeights(Dictionary<string, (int Weight, List<string> Children)> programs, string current)
        {
            var (weight, children) = programs[current];
            if (children.Count == 0)
            {
                return (false, 0, weight);
            }
            var childWeights = new Dictionary<string, int>();
            foreach (var child in children)
            {
                var weights = CheckWeights(programs, child);
                if (weights.Found)
                {
                    return weights;
                }
                childWeights.Add(child, weights.SummedWeight);
            }
            if (childWeights.Values.Distinct().Count() > 1)
            {
                string child;
                int difference;
                if (childWeights.Values.Count(weight => weight == childWeights.Values.Max()) > 1)
                {
                    child = childWeights.Where(childWeight => childWeight.Value == childWeights.Values.Min()).Single().Key;
                    difference = childWeights.Values.Max() - childWeights.Values.Min();
                }
                else
                {
                    child = childWeights.Where(childWeight => childWeight.Value == childWeights.Values.Max()).Single().Key;
                    difference = childWeights.Values.Min() - childWeights.Values.Max();
                }
                return (true, programs[child].Weight + difference, 0);
            }
            return (false, 0, weight + childWeights.Values.Sum());
        }
    }
}
