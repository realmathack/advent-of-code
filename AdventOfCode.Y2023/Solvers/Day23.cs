namespace AdventOfCode.Y2023.Solvers
{
    public class Day23 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            var root = new Node(new Coords(1, 0), []);
            var queue = new Queue<(Node From, Coords Next)>();
            queue.Enqueue((root, FindNeighbors(grid, root.Position).Single()));
            while (queue.TryDequeue(out var branch))
            {
                var cameFrom = branch.From.Position;
                var current = branch.Next;
                var weight = 0;
                while (true)
                {
                    weight++;
                    var neighbors = FindNeighbors(grid, current).Where(neighbor => neighbor != cameFrom).ToArray();
                    if (neighbors.Length == 1)
                    {
                        cameFrom = current;
                        current = neighbors[0];
                    }
                    else
                    {
                        var to = new Node(current, []);
                        branch.From.Edges[to] = weight;
                        foreach (var neighbor in neighbors)
                        {
                            queue.Enqueue((to, neighbor));
                        }
                        break;
                    }
                }
            }
            return FindLongestPath(root, []);
        }

        public override object SolvePart2(char[][] grid)
        {
            var root = new Node(new Coords(1, 0), []);
            var nodes = new Dictionary<Coords, Node>() { [root.Position] = root };
            var queue = new Queue<(Node From, Coords Next)>();
            queue.Enqueue((root, FindNeighbors(grid, root.Position).Single()));
            while (queue.TryDequeue(out var branch))
            {
                var cameFrom = branch.From.Position;
                var current = branch.Next;
                var weight = 0;
                while (true)
                {
                    weight++;
                    var neighbors = FindNeighborsNonSlippery(grid, current).Where(neighbor => neighbor != cameFrom).ToArray();
                    if (neighbors.Length == 1)
                    {
                        cameFrom = current;
                        current = neighbors[0];
                    }
                    else
                    {
                        if (!nodes.TryGetValue(current, out var to))
                        {
                            to = new Node(current, []);
                            nodes.Add(current, to);
                        }
                        else
                        {
                            neighbors = [];
                        }
                        to.Edges[branch.From] = weight;
                        branch.From.Edges[to] = weight;
                        foreach (var neighbor in neighbors)
                        {
                            queue.Enqueue((to, neighbor));
                        }
                        break;
                    }
                }
            }
            return FindLongestPath(root, []);
        }

        private static int FindLongestPath(Node current, HashSet<Node> visited)
        {
            var distances = new List<int>();
            foreach (var edge in current.Edges)
            {
                if (!visited.Add(edge.Key))
                {
                    continue;
                }
                distances.Add(edge.Value + FindLongestPath(edge.Key, visited));
                visited.Remove(edge.Key);
            }
            return distances.Count == 0 ? 0 : distances.Max();
        }

        private static readonly Dictionary<string, char[]> _blocked = new()
        {
            ["west"]  = ['#', '>'],
            ["north"] = ['#', 'v'],
            ["east"]  = ['#', '<'],
            ["south"] = ['#', '^']
        };
        private static List<Coords> FindNeighbors(char[][] grid, Coords node)
        {
            var neighbors = new List<Coords>();
            if (node.X > 0                       && (!_blocked["west"].Contains(grid[node.Y][node.X - 1])) ) { neighbors.Add(node.Left); }
            if (node.Y > 0                       && (!_blocked["north"].Contains(grid[node.Y - 1][node.X]))) { neighbors.Add(node.Up); }
            if (node.X < grid[node.Y].Length - 1 && (!_blocked["east"].Contains(grid[node.Y][node.X + 1])) ) { neighbors.Add(node.Right); }
            if (node.Y < grid.Length - 1         && (!_blocked["south"].Contains(grid[node.Y + 1][node.X]))) { neighbors.Add(node.Down); }
            return neighbors;
        }

        private static List<Coords> FindNeighborsNonSlippery(char[][] grid, Coords node)
        {
            var neighbors = new List<Coords>();
            if (node.X > 0                       && (grid[node.Y][node.X - 1] != '#')) { neighbors.Add(node.Left); }
            if (node.Y > 0                       && (grid[node.Y - 1][node.X] != '#')) { neighbors.Add(node.Up); }
            if (node.X < grid[node.Y].Length - 1 && (grid[node.Y][node.X + 1] != '#')) { neighbors.Add(node.Right); }
            if (node.Y < grid.Length - 1         && (grid[node.Y + 1][node.X] != '#')) { neighbors.Add(node.Down); }
            return neighbors;
        }

        private record class Node(Coords Position, Dictionary<Node, int> Edges);
    }
}
