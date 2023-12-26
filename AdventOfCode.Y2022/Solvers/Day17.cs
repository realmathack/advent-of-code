namespace AdventOfCode.Y2022.Solvers
{
    public class Day17 : SolverWithText
    {
        public override object SolvePart1(string input) => CalculateHeights(input, 2022);

        public override object SolvePart2(string input) => CalculateHeights(input, 1000000000000);

        private static long CalculateHeights(string jets, long rocks)
        {
            var jetIndex = 0;
            var currentHeight = 0L;
            var knownResults = new Dictionary<string, (long Rock, long Height)>();
            var fallen = new HashSet<LongCoords>();
            for (long rock = 0L; rock < rocks; rock++)
            {
                var shape = GetShape(rock, currentHeight);
                while (true)
                {
                    var tmp = (jets[jetIndex] == '<') ? shape.Select(coord => coord.Left).ToList() : shape.Select(coord => coord.Right).ToList();
                    if (tmp.All(coord => coord.X > -1 && coord.X < 7 && !fallen.Contains(coord)))
                    {
                        shape = tmp;
                    }
                    jetIndex = (jetIndex + 1) % jets.Length;
                    tmp = shape.Select(coord => coord.Up).ToList();
                    if (tmp.Any(coord => coord.Y <= 0 || fallen.Contains(coord)))
                    {
                        fallen.UnionWith(shape);
                        var height = shape.Max(coord => coord.Y);
                        if (height > currentHeight)
                        {
                            currentHeight = height;
                        }
                        break;
                    }
                    shape = tmp;
                }
                var key = $"{rock % 5L}|{jetIndex}|{GetTopRows(fallen, currentHeight)}";
                if (knownResults.TryGetValue(key, out var result))
                {
                    var deltaHeight = currentHeight - result.Height;
                    var deltaRocks = rock - result.Rock;
                    var cycles = (rocks - rock) / deltaRocks;
                    currentHeight += cycles * deltaHeight;
                    rock += cycles * deltaRocks;
                    var offset = new LongCoords(0, cycles * deltaHeight);
                    fallen = fallen.Select(coord => coord + offset).ToHashSet();
                }
                else
                {
                    knownResults.Add(key, (rock, currentHeight));
                }
            }
            return currentHeight;
        }

        private static string GetTopRows(HashSet<LongCoords> fallen, long currentHeight)
        {
            var topRows = new List<string>();
            for (long y = currentHeight; y > currentHeight - 20; y--)
            {
                topRows.Add(string.Concat(Enumerable.Range(0, 7).Select(x => fallen.Contains(new(x, y)) ? '#' : '.')));
            }
            return string.Concat(topRows);
        }

        private static List<LongCoords> GetShape(long rock, long height)
        {
            return (rock % 5L) switch
            {
                0L => [new(2, height + 4), new(3, height + 4), new(4, height + 4), new(5, height + 4)],
                1L => [new(3, height + 6), new(2, height + 5), new(3, height + 5), new(4, height + 5), new(3, height + 4)],
                2L => [new(4, height + 6), new(4, height + 5), new(2, height + 4), new(3, height + 4), new(4, height + 4)],
                3L => [new(2, height + 7), new(2, height + 6), new(2, height + 5), new(2, height + 4)],
                4L => [new(2, height + 5), new(3, height + 5), new(2, height + 4), new(3, height + 4)],
                _ => throw new InvalidOperationException()
            };
        }

        private readonly record struct LongCoords(long X, long Y)
        {
            public static LongCoords operator +(LongCoords a, LongCoords b) => new(a.X + b.X, a.Y + b.Y);
            public LongCoords Left => new(X - 1, Y);
            public LongCoords Right => new(X + 1, Y);
            public LongCoords Up => new(X, Y - 1);
        }
    }
}
