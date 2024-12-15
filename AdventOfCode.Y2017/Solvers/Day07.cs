using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Solvers
{
    public partial class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var programs = new HashSet<string>();
            var childeren = new HashSet<string>();
            foreach (var line in input)
            {
                var match = ProgramRegex().Match(line);
                if (match.Groups[4].Length > 0)
                {
                    childeren.UnionWith(match.Groups[4].Value.Split(", "));
                }
                programs.Add(match.Groups[1].Value);
            }
            return programs.Except(childeren).Single();
        }

        public override object SolvePart2(string[] input)
        {
            var programs = new Dictionary<string, (int Weight, string[] Children)>();
            var alllChilderen = new HashSet<string>();
            foreach (var line in input)
            {
                var match = ProgramRegex().Match(line);
                var children = (match.Groups[4].Length > 0) ? match.Groups[4].Value.Split(", ").ToArray() : [];
                alllChilderen.UnionWith(children);
                programs.Add(match.Groups[1].Value, (int.Parse(match.Groups[2].Value), children));
            }
            var root = programs.Keys.ToHashSet().Except(alllChilderen).Single();
            var (found, correctedWeight, _) = CheckWeights(programs, root);
            if (!found)
            {
                throw new InvalidOperationException("Solution not found");
            }
            return correctedWeight;
        }

        private static (bool Found, int CorrectedWeight, int SummedWeight) CheckWeights(Dictionary<string, (int Weight, string[] Children)> programs, string current)
        {
            var (weight, children) = programs[current];
            if (children.Length == 0)
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

        [GeneratedRegex(@"(.+) \((\d+)\)( -> (.+))?")]
        private static partial Regex ProgramRegex();
    }
}
