namespace AdventOfCode.Graphs
{
    public abstract class BFS<T>
        where T : notnull
    {
        public List<T> Search(T root)
        {
            var visited = new HashSet<T>() { root };
            var cameFrom = new Dictionary<T, T>();
            var queue = new Queue<T>();
            queue.Enqueue(root);
            while (queue.TryDequeue(out var current))
            {
                if (IsGoal(current))
                {
                    return ReconstructPath(cameFrom, current);
                }
                foreach (var neighbor in FindNeighbors(current))
                {
                    if (!visited.Add(neighbor))
                    {
                        continue;
                    }
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
            return [];
        }

        protected abstract List<T> FindNeighbors(T node);
        protected abstract bool IsGoal(T node);

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
