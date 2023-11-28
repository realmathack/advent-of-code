namespace AdventOfCode.Y2015.Solvers
{
    public class Day13 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return ToNodes(input).Select(node => GetHapiness(node, [node])).Max();
        }

        public override object SolvePart2(string[] input)
        {
            var nodes = ToNodes(input);
            var nodeSelf = new Node("Me");
            foreach (var node in nodes)
            {
                nodeSelf.Neighbors.Add(node, 0);
                node.Neighbors.Add(nodeSelf, 0);
            }
            return nodes.Select(node => GetHapiness(node, [node])).Max();
        }

        private static int GetHapiness(Node node, List<Node> visited)
        {
            var distances = new List<int>();
            foreach (var neighbor in node.Neighbors)
            {
                if (visited.Contains(neighbor.Key))
                {
                    continue;
                }
                visited.Add(neighbor.Key);
                distances.Add(neighbor.Value + neighbor.Key.Neighbors[node] + GetHapiness(neighbor.Key, visited));
                visited.Remove(neighbor.Key);
            }
            return (distances.Count == 0) ? node.Neighbors[visited[0]] + visited[0].Neighbors[node] : distances.Max();
        }

        private static List<Node> ToNodes(string[] lines)
        {
            var nodes = new Dictionary<string, Node>();
            foreach (var line in lines)
            {
                var parts = line.TrimEnd('.').Split(' ');
                var (person, amount, neighbor) = (parts[0], int.Parse((parts[2] == "lose" ? "-" : null) + parts[3]), parts[10]);
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

        private class Node(string name)
        {
            public string Name { get; } = name;
            public Dictionary<Node, int> Neighbors { get; } = [];
        }
    }
}
