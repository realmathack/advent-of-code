namespace AdventOfCode.Y2023.Solvers
{
    public class Day21(int? _steps) : SolverWithCharGrid
    {
        public Day21() : this(null) { }

        public override object SolvePart1(char[][] grid)
        {
            var start = GetStart(grid);
            var steps = _steps ?? 64;
            var queue = new HashSet<Coords>() { start };
            for (int i = 0; i < steps; i++)
            {
                var next = new HashSet<Coords>();
                foreach (var current in queue)
                {
                    next.UnionWith(FindPossibleNeighbors(grid, current));
                }
                queue = next;
            }
            return queue.Count;
        }

        // HACK: Implemented https://github.com/jmerle/advent-of-code-2023/blob/master/src/day21/part2.py
        // This only works because of the empty x/y-axis in the real input, test input would need this solution:
        // https://github.com/maksverver/AdventOfCode/blob/master/2023/day21/solve.py
        public override object SolvePart2(char[][] grid)
        {
            var start = GetStart(grid);
            var steps = _steps ?? 26501365;
            var size = grid.Length;
            var halfSize = (size - 1) / 2;
            var visited = new HashSet<(Coords Position, int Step)>();
            var gardens = new Dictionary<int, HashSet<Coords>>();
            var queue = new Queue<(Coords Position, int Step)>();
            queue.Enqueue((start, 0));
            while (queue.TryDequeue(out var current))
            {
                if (!visited.Add(current))
                {
                    continue;
                }
                var x = (size + (current.Position.X % size)) % size;
                var y = (size + (current.Position.Y % size)) % size;
                if (grid[y][x] != '.')
                {
                    continue;
                }
                if ((current.Step - halfSize) % size == 0)
                {
                    if (!gardens.TryGetValue(current.Step, out var garden))
                    {
                        garden = [];
                        gardens[current.Step] = garden;
                    }
                    garden.Add(current.Position);
                }
                if (current.Step == 2 * size + halfSize)
                {
                    continue; // Should have cycles now, don't go further out
                }
                foreach (var neighbor in current.Position.Neighbors)
                {
                    queue.Enqueue((neighbor, current.Step + 1));
                }
            }
            var step = 2 * size + halfSize;
            var score = (long)gardens[step].Count;
            var totalIncrement = score - gardens[size + halfSize].Count; // After a while the score increases with a constant amount
            var increment = totalIncrement - (gardens[size + halfSize].Count - gardens[halfSize].Count);
            while (step != steps)
            {
                totalIncrement += increment;
                score += totalIncrement;
                step += size;
            }
            return score;
        }

        private static List<Coords> FindPossibleNeighbors(char[][] grid, Coords node)
        {
            var neighbors = new List<Coords>();
            if (node.X > 0                       && grid[node.Left.Y][node.Left.X] == '.'  ) { neighbors.Add(node.Left); }
            if (node.Y > 0                       && grid[node.Up.Y][node.Up.X] == '.'      ) { neighbors.Add(node.Up); }
            if (node.X < grid[node.Y].Length - 1 && grid[node.Right.Y][node.Right.X] == '.') { neighbors.Add(node.Right); }
            if (node.Y < grid.Length - 1         && grid[node.Down.Y][node.Down.X] == '.'  ) { neighbors.Add(node.Down); }
            return neighbors;
        }

        private static Coords GetStart(char[][] grid)
        {
            var start = new Coords(-1, -1);
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == 'S')
                    {
                        start = new(x, y);
                        grid[y][x] = '.';
                    }
                }
            }
            return start;
        }
    }
}
