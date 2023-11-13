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
                    foreach (var child in parts[1].Split(", "))
                    {
                        childeren.Add(child);
                    }
                }
                programs.Add(parts[0].Split(' ')[0]);
            }
            return programs.Except(childeren).Single();
        }

        public override object SolvePart2(string[] input)
        {
            var programs = new Dictionary<string, (int weight, List<string> children)>();
            var alllChilderen = new List<string>();
            foreach (var line in input)
            {
                var parts = line.Split(" -> ");
                var children = (parts.Length == 1) ? new List<string>() : parts[1].Split(", ").ToList();
                alllChilderen.AddRange(children);
                parts = parts[0].Split(' ');
                programs.Add(parts[0], (int.Parse(parts[1].Trim('(', ')')), children));
            }
            var root = programs.Keys.ToArray().Except(alllChilderen).Single();
            var (found, correctedWeight, _) = CheckWeights(programs, root);
            if (!found)
            {
                throw new InvalidOperationException("Solution not found");
            }
            return correctedWeight;
        }

        private static (bool found, int correctedWeight, int summedWeight) CheckWeights(Dictionary<string, (int weight, List<string> children)> programs, string current)
        {
            var (weight, children) = programs[current];
            if (children.Count == 0)
            {
                return (false, 0, weight);
            }
            var childWeights = new Dictionary<string, int>();
            foreach (var child in children)
            {
                var result = CheckWeights(programs, child);
                if (result.found)
                {
                    return result;
                }
                childWeights.Add(child, result.summedWeight);
            }
            if (childWeights.Values.Distinct().Count() > 1)
            {
                string child;
                int difference;
                if (childWeights.Values.Count(x => x == childWeights.Values.Max()) > 1)
                {
                    child = childWeights.Where(x => x.Value == childWeights.Values.Min()).Single().Key;
                    difference = childWeights.Values.Max() - childWeights.Values.Min();
                }
                else
                {
                    child = childWeights.Where(x => x.Value == childWeights.Values.Max()).Single().Key;
                    difference = childWeights.Values.Min() - childWeights.Values.Max();
                }
                return (true, programs[child].weight + difference, 0);
            }
            return (false, 0, weight + childWeights.Values.Sum());
        }
    }
}
