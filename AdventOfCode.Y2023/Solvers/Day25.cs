namespace AdventOfCode.Y2023.Solvers
{
    public class Day25 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (nodes, edges) = ToComponents(input);
            var count = nodes.Count;
            var minCutSize = FindWithKrager(nodes, edges);
            return minCutSize * (count - minCutSize);
        }

        public override object SolvePart2(string[] input) => "Day 25";

        // https://en.wikipedia.org/wiki/Karger%27s_algorithm
        private static int FindWithKrager(HashSet<string> nodes, Edge[] edges)
        {
            while (true)
            {
                var (cuts, minCutSize) = Contract(nodes, edges);
                if (cuts == 3)
                {
                    return minCutSize;
                }
            }
        }

        private static (int Cuts, int MinCutSize) Contract(HashSet<string> originalNodes, Edge[] originalEdges)
        {
            var nodes = new HashSet<string>(originalNodes);
            var edges = new List<Edge>(originalEdges.Length);
            foreach (var edge in originalEdges)
            {
                edges.Add(new(edge.From, edge.To));
            }
            var mergedNodes = nodes.ToDictionary(node => node, node => new HashSet<string> { node });
            while (nodes.Count > 2)
            {
                var edgeToCut = edges[Random.Shared.Next(edges.Count)];
                var nodeToMerge = edgeToCut.From;
                nodes.Remove(nodeToMerge);
                mergedNodes[edgeToCut.To].UnionWith(mergedNodes[nodeToMerge]);
                mergedNodes.Remove(nodeToMerge);
                for (int i = edges.Count - 1; i >= 0; i--)
                {
                    if (edges[i].From != nodeToMerge && edges[i].To != nodeToMerge)
                    {
                        continue;
                    }
                    var tmp = edges[i].To == nodeToMerge ? edges[i].From : edges[i].To;
                    if (tmp != edgeToCut.To)
                    {
                        edges.Add(new(edgeToCut.To, tmp));
                    }
                    edges.RemoveAt(i);
                }
            }
            var sets = mergedNodes.Values.ToArray();
            var cuts = 0;
            foreach (var edge in originalEdges)
            {
                if ((sets[0].Contains(edge.From) && sets[1].Contains(edge.To)) || (sets[0].Contains(edge.To) && sets[1].Contains(edge.From)))
                {
                    cuts++;
                }
            }
            return (cuts, sets[0].Count);
        }

        private static (HashSet<string> Nodes, Edge[] Edges) ToComponents(string[] lines)
        {
            var nodes = new HashSet<string>();
            var edges = new HashSet<Edge>();
            foreach (var line in lines)
            {
                var (name, connections) = line.SplitInTwo(": ");
                nodes.Add(name);
                foreach (var connection in connections.Split(' '))
                {
                    nodes.Add(connection);
                    edges.Add(new(name, connection));
                }
            }
            return (nodes, edges.ToArray());
        }

        private record class Edge(string From, string To);
    }
}
