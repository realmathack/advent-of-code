namespace AdventOfCode.Y2022.Solvers
{
    public class Day09 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return GetTailVisitedCount(input, 2);
        }

        public override object SolvePart2(string[] input)
        {
            return GetTailVisitedCount(input, 10);
        }

        private static int GetTailVisitedCount(string[] lines, int segmentCount)
        {
            var segments = new Coords[segmentCount];
            for (int i = 0; i < segmentCount; i++)
            {
                segments[i] = new Coords(0, 0);
            }
            var tailVisited = new HashSet<Coords> { segments[^1] };
            foreach (var motion in ToMotions(lines))
            {
                for (int step = 0; step < motion.Steps; step++)
                {
                    segments[0] = motion.Direction switch
                    {
                        'L' => segments[0].Left,
                        'U' => segments[0].Up,
                        'R' => segments[0].Right,
                        'D' => segments[0].Down,
                        _ => throw new InvalidOperationException($"Unknown direction: {motion.Direction}")
                    };
                    for (int i = 1; i < segmentCount; i++)
                    {
                        if (!segments[i - 1].IsNeighbor(segments[i]))
                        {
                            var offsetX = CalculateOffset(segments[i - 1].X, segments[i].X);
                            var offsetY = CalculateOffset(segments[i - 1].Y, segments[i].Y);
                            segments[i] += (offsetX, offsetY);
                            if (i == segmentCount - 1)
                            {
                                tailVisited.Add(segments[i]);
                            }
                        }
                    }
                }
            }
            return tailVisited.Count;
        }

        private static List<Motion> ToMotions(string[] lines) => lines.Select(line => new Motion(line[0], int.Parse(line[2..]))).ToList();
        private static int CalculateOffset(int previousAxis, int currentAxis) => (previousAxis - currentAxis == 0) ? 0 : (previousAxis - currentAxis > 0) ? 1 : -1;

        private record struct Motion(char Direction, int Steps);
    }
}
