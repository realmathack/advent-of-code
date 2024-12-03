namespace AdventOfCode.Graphs
{
    // https://en.wikipedia.org/wiki/A*_search_algorithm
    public abstract class AStar<T>
        where T : notnull
    {
        public List<T> FindShortestPath(T start, T goal)
        {
            var visited = new HashSet<T>();
            var cameFrom = new Dictionary<T, T>();
            var distances = new Dictionary<T, int>() { [start] = 0 };
            var queue = new PriorityQueue<T, int>();
            queue.Enqueue(start, CalculateHeuristic(start, goal));
            while (queue.TryDequeue(out var current, out _))
            {
                if (current.Equals(goal))
                {
                    return ReconstructPath(cameFrom, current);
                }
                visited.Add(current);
                foreach (var neighbor in FindNeighbors(current))
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    var tmpDistance = distances[current] + GetEdgeWeight(current, neighbor);
                    if (tmpDistance < GetDistance(distances, neighbor))
                    {
                        cameFrom[neighbor] = current;
                        distances[neighbor] = tmpDistance;
                        queue.Enqueue(neighbor, tmpDistance + CalculateHeuristic(neighbor, goal));
                    }
                }
            }
            return [];
        }

        protected virtual int GetEdgeWeight(T node1, T node2) => 1;
        protected abstract int CalculateHeuristic(T node, T goal);
        protected abstract List<T> FindNeighbors(T node);

        private static List<T> ReconstructPath(Dictionary<T, T> cameFrom, T node)
        {
            var path = new List<T>();
            while (cameFrom.TryGetValue(node, out node!))
            {
                path.Add(node);
            }
            path.Reverse();
            return path;
        }

        private static int GetDistance(Dictionary<T, int> distances, T node) => distances.TryGetValue(node, out var distance) ? distance : int.MaxValue;
    }
}
