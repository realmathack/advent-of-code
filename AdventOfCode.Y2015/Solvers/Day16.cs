using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public partial class Day16 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var searchParameters = GetSearchParameters();
            foreach (var sue in ToSues(input))
            {
                var isMatch = true;
                foreach (var property in sue.Properties)
                {
                    if (searchParameters.TryGetValue(property.Key, out var value))
                    {
                        isMatch &= (property.Value == value);
                    }
                }
                if (isMatch)
                {
                    return sue.Number;
                }
            }
            return 0;
        }

        public override object SolvePart2(string[] input)
        {
            var searchParameters = GetSearchParameters();
            foreach (var sue in ToSues(input))
            {
                var isMatch = true;
                foreach (var property in sue.Properties)
                {
                    if (searchParameters.TryGetValue(property.Key, out var value))
                    {
                        if (property.Key.Contains("cats") || property.Key.Contains("trees"))
                        {
                            isMatch &= (property.Value > value);
                        }
                        else if (property.Key.Contains("pomeranians") || property.Key.Contains("goldfish"))
                        {
                            isMatch &= (property.Value < value);
                        }
                        else
                        {
                            isMatch &= (property.Value == value);
                        }
                    }
                }
                if (isMatch)
                {
                    return sue.Number;
                }
            }
            return 0;
        }

        private static Dictionary<string, int> GetSearchParameters()
        {
            var parameters = new Dictionary<string, int>();
            var lines = @"children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1
".SplitIntoLines();
            foreach (var line in lines)
            {
                var parts = line.Split(": ");
                parameters.Add(parts[0], int.Parse(parts[1]));
            }
            return parameters;
        }

        private static List<Sue> ToSues(string[] lines)
        {
            var sues = new List<Sue>(lines.Length);
            foreach (var line in lines)
            {
                var match = SueRegex().Match(line);
                var sue = new Sue(int.Parse(match.Groups[1].Value), []);
                foreach (var property in match.Groups[2].Value.Split(", "))
                {
                    var (name, value) = property.SplitInTwo(": ");
                    sue.Properties.Add(name, int.Parse(value));
                }
                sues.Add(sue);
            }
            return sues;
        }

        [GeneratedRegex(@"Sue (\d+): (.+)")]
        private static partial Regex SueRegex();

        private record class Sue(int Number, Dictionary<string, int> Properties);
    }
}
