namespace AdventOfCode.Graphs
{
    // https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
    public abstract class Dijkstra<T>
        where T : notnull
    {
        public List<T> FindShortestPath(T start)
        {
            var visited = new HashSet<T>();
            var cameFrom = new Dictionary<T, T>();
            var distances = new Dictionary<T, int>() { [start] = 0 };
            var queue = new PriorityQueue<T, int>();
            queue.Enqueue(start, 0);
            while (queue.TryDequeue(out var current, out _))
            {
                if (IsGoal(current))
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
                        queue.Enqueue(neighbor, tmpDistance);
                    }
                }
            }
            return [];
        }

        public Dictionary<T, int> GetShortestDistances(T start, IEnumerable<T> nodes)
        {
            var visited = new HashSet<T>();
            var cameFrom = new Dictionary<T, T>();
            var distances = new Dictionary<T, int>() { [start] = 0 };
            var queue = new PriorityQueue<T, int>();
            foreach (var node in nodes)
            {
                queue.Enqueue(node, GetDistance(distances, node));
            }
            while (queue.TryDequeue(out var current, out _))
            {
                if (!visited.Add(current))
                {
                    continue;
                }
                foreach (var neighbor in FindNeighbors(current))
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    var tmpDistance = distances.TryGetValue(current, out var distance) ? distance + GetEdgeWeight(current, neighbor) : int.MaxValue;
                    if (tmpDistance < GetDistance(distances, neighbor))
                    {
                        cameFrom[neighbor] = current;
                        distances[neighbor] = tmpDistance;
                        queue.Enqueue(neighbor, tmpDistance);
                    }
                }
            }
            return distances;
        }

        protected virtual int GetEdgeWeight(T node1, T node2) => 1;
        protected abstract List<T> FindNeighbors(T node);
        protected virtual bool IsGoal(T node) => true;

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
