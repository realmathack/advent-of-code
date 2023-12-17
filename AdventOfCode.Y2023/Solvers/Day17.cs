namespace AdventOfCode.Y2023.Solvers
{
    public class Day17 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => FindLeastHeatPath(input, QueuePath);
        public override object SolvePart2(string[] input) => FindLeastHeatPath(input, QueuePathUltra);

        private static int FindLeastHeatPath(string[] input, Action<PriorityQueue<Move, int>, Move, int, Coords> queueMethod)
        {
            var grid = ToGrid(input);
            var start = new Coords(0, 0);
            var goal = new Coords(grid.Length - 1, grid[0].Length - 1);
            var visited = new HashSet<Move>();
            var queue = new PriorityQueue<Move, int>();
            queue.Enqueue(new(start, Coords.OffsetRight, 1), 0);
            queue.Enqueue(new(start, Coords.OffsetDown, 1), 0);
            while (queue.TryDequeue(out var current, out var heat))
            {
                if (!visited.Add(current))
                {
                    continue;
                }
                if (current.Position == goal)
                {
                    return heat;
                }
                var nextPosition = current.Position + current.Direction;
                if (nextPosition.Y < 0 || nextPosition.X < 0 || nextPosition.Y >= grid.Length || nextPosition.X >= grid[0].Length)
                {
                    continue;
                }
                heat += grid[nextPosition.Y][nextPosition.X];
                queueMethod(queue, current, heat, nextPosition);
            }
            return 0;
        }

        private static void QueuePath(PriorityQueue<Move, int> queue, Move current, int heat, Coords nextPosition)
        {
            if (current.CountSameDirection < 3)
            {
                queue.Enqueue(new(nextPosition, current.Direction, current.CountSameDirection + 1), heat);
            }
            queue.Enqueue(new(nextPosition, current.Direction.RotateLeft, 1), heat);
            queue.Enqueue(new(nextPosition, current.Direction.RotateRight, 1), heat);
        }

        private static void QueuePathUltra(PriorityQueue<Move, int> queue, Move current, int heat, Coords nextPosition)
        {
            if (current.CountSameDirection < 10)
            {
                queue.Enqueue(new(nextPosition, current.Direction, current.CountSameDirection + 1), heat);
            }
            if (current.CountSameDirection >= 4)
            {
                queue.Enqueue(new(nextPosition, current.Direction.RotateLeft, 1), heat);
                queue.Enqueue(new(nextPosition, current.Direction.RotateRight, 1), heat);
            }
        }

        private static int[][] ToGrid(string[] input) => input.Select(line => line.Select(block => block - '0').ToArray()).ToArray();

        private record class Move(Coords Position, Coords Direction, int CountSameDirection);
    }
}
