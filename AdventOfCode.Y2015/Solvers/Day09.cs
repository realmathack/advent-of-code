namespace AdventOfCode.Y2015.Solvers
{
    public class Day09 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToNodes(input).Min(node => CalculateDistance(node, [node]));
        public override object SolvePart2(string[] input) => ToNodes(input).Max(node => CalculateDistance(node, [node], true));

        private static List<Node> ToNodes(string[] lines)
        {
            var nodes = new Dictionary<string, Node>();
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var (from, to, distance) = (parts[0], parts[2], int.Parse(parts[4]));
                if (!nodes.TryGetValue(from, out var nodeFrom))
                {
                    nodeFrom = new Node(from);
                    nodes.Add(from, nodeFrom);
                }
                if (!nodes.TryGetValue(to, out var nodeTo))
                {
                    nodeTo = new Node(to);
                    nodes.Add(to, nodeTo);
                }
                nodeFrom.Routes.Add(nodeTo, distance);
                nodeTo.Routes.Add(nodeFrom, distance);
            }
            return [.. nodes.Values];
        }

        private static int CalculateDistance(Node node, HashSet<Node> visited, bool longestRoute = false)
        {
            var distances = new List<int>();
            foreach (var route in node.Routes)
            {
                if (visited.Contains(route.Key))
                {
                    continue;
                }
                visited.Add(route.Key);
                distances.Add(route.Value + CalculateDistance(route.Key, visited, longestRoute));
                visited.Remove(route.Key);
            }
            return (distances.Count == 0) ? 0 : (longestRoute) ? distances.Max() : distances.Min();
        }

        private record class Node(string Name)
        {
            public Dictionary<Node, int> Routes { get; } = [];
        }
    }
}
