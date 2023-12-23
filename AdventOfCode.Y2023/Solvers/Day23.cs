namespace AdventOfCode.Y2023.Solvers
{
    public class Day23 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var grid = input.ToCharGrid();
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
                    var neighbors = FindNeighbors(grid, current).Where(neighbor => neighbor != cameFrom).ToList();
                    if (neighbors.Count == 1)
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
            var end = new Coords(grid[0].Length - 2, grid.Length - 1);
            return FindLongestPath(root, end);
        }

        public override object SolvePart2(string[] input)
        {
            var grid = input.ToCharGrid();
            var root = new Node(new Coords(1, 0), []);
            var nodes = new Dictionary<Coords, Node>() { { root.Position, root } };
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
                    var neighbors = FindNeighborsNonSlippery(grid, current).Where(neighbor => neighbor != cameFrom).ToList();
                    if (neighbors.Count == 1)
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
                            neighbors.Clear();
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
            var end = new Coords(grid[0].Length - 2, grid.Length - 1);
            return FindLongestPath(root, end);
        }

        private static int FindLongestPath(Node root, Coords end)
        {
            var largest = 0;
            var dfs = new Stack<Node>();
            var bfs = new Queue<(Node Next, int TotalWeight, HashSet<Node> Path)>();
            bfs.Enqueue((root, 0, []));
            while (bfs.TryDequeue(out var current))
            {
                foreach (var edge in current.Next.Edges.Where(edge => !current.Path.Contains(edge.Key)))
                {
                    var tmp = current.TotalWeight + edge.Value;
                    if (edge.Key.Position == end && tmp > largest)
                    {
                        largest = tmp;
                    }
                    bfs.Enqueue((edge.Key, tmp, [.. current.Path, edge.Key]));
                }
            }
            return largest;
        }

        private static readonly Dictionary<string, char[]> _blocked = new()
        {
            { "west" , ['#', '>'] },
            { "north", ['#', 'v'] },
            { "east" , ['#', '<'] },
            { "south", ['#', '^'] }
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
