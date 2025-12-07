using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2025.Solvers
{
    public class Day07 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] input)
        {
            var splits = 0;
            var visited = new HashSet<Coords>();
            var queue = new Queue<Coords>();
            var start = new Coords(Array.IndexOf(input[0], 'S'), 0);
            queue.Enqueue(start);
            while (queue.TryDequeue(out var current))
            {
                if (!visited.Add(current))
                {
                    continue;
                }
                var nextY = current.Y + 2;
                if (nextY == input.Length)
                {
                    continue;
                }
                var next = input[nextY][current.X];
                if (next == '^')
                {
                    splits++;
                    queue.Enqueue(new(current.X - 1, nextY));
                    queue.Enqueue(new(current.X + 1, nextY));
                }
                else
                {
                    queue.Enqueue(new(current.X, nextY));
                }
            }
            return splits;
        }

        public override object SolvePart2(char[][] input)
        {
            var cache = new Dictionary<Coords, long>();
            var start = new Coords(Array.IndexOf(input[0], 'S'), 0);
            return Part2Inner(cache, input, start);
        }

        private static long Part2Inner(Dictionary<Coords, long> cache, char[][] input, Coords current)
        {
            if (current.Y == input.Length)
            {
                return 1L;
            }
            if (cache.TryGetValue(current, out var count))
            {
                return count;
            }
            var nextY = current.Y + 2;
            var item = input[current.Y][current.X];
            if (item == '^')
            {
                count = Part2Inner(cache, input, new(current.X - 1, nextY)) + Part2Inner(cache, input, new(current.X + 1, nextY));
                cache[current] = count;
                return count;
            }
            return Part2Inner(cache, input, new(current.X, nextY));
        }
    }
}
