using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public partial class Day13 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToNodes(input).Max(node => CalculateHapiness(node, node, [node]));

        public override object SolvePart2(string[] input)
        {
            var nodes = ToNodes(input);
            var nodeSelf = new Node("Me");
            foreach (var node in nodes)
            {
                nodeSelf.Neighbors.Add(node, 0);
                node.Neighbors.Add(nodeSelf, 0);
            }
            return nodes.Max(node => CalculateHapiness(node, node, [node]));
        }

        private static List<Node> ToNodes(string[] lines)
        {
            var nodes = new Dictionary<string, Node>();
            foreach (var line in lines)
            {
                var match = HapinessRegex().Match(line);
                var (person, amount, neighbor) = (match.Groups[1].Value, int.Parse((match.Groups[2].Value == "lose" ? "-" : null) + match.Groups[3].Value), match.Groups[4].Value);
                if (!nodes.TryGetValue(person, out var nodePerson))
                {
                    nodePerson = new Node(person);
                    nodes.Add(person, nodePerson);
                }
                if (!nodes.TryGetValue(neighbor, out var nodeNeighbor))
                {
                    nodeNeighbor = new Node(neighbor);
                    nodes.Add(neighbor, nodeNeighbor);
                }
                nodePerson.Neighbors.Add(nodeNeighbor, amount);
            }
            return [.. nodes.Values];
        }

        private static int CalculateHapiness(Node start, Node node, HashSet<Node> visited)
        {
            var distances = new List<int>();
            foreach (var neighbor in node.Neighbors)
            {
                if (!visited.Add(neighbor.Key))
                {
                    continue;
                }
                distances.Add(neighbor.Value + neighbor.Key.Neighbors[node] + CalculateHapiness(start, neighbor.Key, visited));
                visited.Remove(neighbor.Key);
            }
            return (distances.Count == 0) ? node.Neighbors[start] + start.Neighbors[node] : distances.Max();
        }

        [GeneratedRegex(@"(.+) would (lose|gain) (\d+) happiness units by sitting next to (.+)\.")]
        public static partial Regex HapinessRegex();

        private record class Node(string Name)
        {
            public Dictionary<Node, int> Neighbors { get; } = [];
        }
    }
}
