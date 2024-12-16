using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day16 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            var (start, end) = GetStartAndEnd(grid);
            return new Dijkstra(grid, end).FindShortestPath(start).Distance;
        }

        public override object SolvePart2(char[][] grid)
        {
            var (start, end) = GetStartAndEnd(grid);
            return new MultiPathDijkstra(grid, end).FindAllTilesInShortestPaths(start).Count;
        }

        private static List<Node> FindPossibleMovements(char[][] grid, Node node)
        {
            var movements = new List<Node>();
            var next = node.Position + node.Direction;
            if (!grid.IsOutOfBounds(next) && grid[next.Y][next.X] != '#')
            {
                movements.Add(new(next, node.Direction));
            }
            movements.Add(new(node.Position, node.Direction.RotateLeft));
            movements.Add(new(node.Position, node.Direction.RotateRight));
            return movements;
        }

        private static int GetEdgeWeight(Node node1, Node node2) => (node1.Direction == node2.Direction) ? 1 : 1000;

        private static (Node Start, Coords End) GetStartAndEnd(char[][] grid)
        {
            var start = new Coords(1, grid.Length - 2);
            var end = new Coords(grid[0].Length - 2, 1);
            return (new(start, Coords.OffsetRight), end);
        }

        private class Dijkstra(char[][] grid, Coords goal) : Graphs.Dijkstra<Node>
        {
            protected override List<Node> FindNeighbors(Node node) => FindPossibleMovements(grid, node);
            protected override bool IsGoal(Node node) => node.Position == goal;
            protected override int GetEdgeWeight(Node node1, Node node2) => Day16.GetEdgeWeight(node1, node2);
        }

        private class MultiPathDijkstra(char[][] grid, Coords goal)
        {
            public HashSet<Coords> FindAllTilesInShortestPaths(Node start)
            {
                var visited = new HashSet<Node>();
                var cameFrom = new Dictionary<Node, List<Node>>();
                var distances = new Dictionary<Node, int>() { [start] = 0 };
                var queue = new PriorityQueue<Node, int>();
                queue.Enqueue(start, 0);
                while (queue.TryDequeue(out var current, out _))
                {
                    visited.Add(current);
                    foreach (var neighbor in FindPossibleMovements(grid, current))
                    {
                        if (visited.Contains(neighbor))
                        {
                            continue;
                        }
                        var tmpDistance = distances[current] + GetEdgeWeight(current, neighbor);
                        var currentDistance = GetDistance(distances, neighbor);
                        if (tmpDistance < currentDistance)
                        {
                            cameFrom[neighbor] = [current];
                            distances[neighbor] = tmpDistance;
                            queue.Enqueue(neighbor, tmpDistance);
                        }
                        else if (tmpDistance == currentDistance)
                        {
                            cameFrom[neighbor].Add(current);
                        }
                    }
                }
                visited.Clear();
                var goalNodes = Coords.NeighborOffsets.Select(direction => new Node(goal, direction)).ToArray();
                var tmpItems = goalNodes.Select(node => (node, distance: GetDistance(distances, node))).ToArray();
                var shortestDistance = tmpItems.Min(item => item.distance);
                goalNodes = tmpItems.Where(item => item.distance == shortestDistance).Select(item => item.node).ToArray();
                var tileQueue = new Queue<Node>(goalNodes);
                visited.UnionWith(goalNodes);
                while (tileQueue.TryDequeue(out var current))
                {
                    if (!cameFrom.TryGetValue(current, out var nodes))
                    {
                        continue;
                    }
                    foreach (var node in nodes)
                    {
                        if (visited.Add(node))
                        {
                            tileQueue.Enqueue(node);
                        }
                    }
                }
                return visited.Select(node => node.Position).ToHashSet();
            }

            private static int GetDistance(Dictionary<Node, int> distances, Node node) => distances.TryGetValue(node, out var distance) ? distance : int.MaxValue;
        }

        private record class Node(Coords Position, Coords Direction);
    }
}
