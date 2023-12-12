namespace AdventOfCode.Graphs
{
    public abstract class BFS<T>
        where T : notnull
    {
        public List<T> FindPath(T start, T goal)
        {
            var visited = new HashSet<T>() { start };
            var cameFrom = new Dictionary<T, T>();
            var queue = new Queue<T>();
            queue.Enqueue(start);
            while (queue.TryDequeue(out var current))
            {
                if (current.Equals(goal))
                {
                    return ReconstructPath(cameFrom, current);
                }
                foreach (var neighbor in FindNeighbors(current))
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
            return [];
        }

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
    }
}
