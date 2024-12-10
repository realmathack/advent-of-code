using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2023.Solvers
{
    public class Day11(int _timesLarger) : SolverWithLines
    {
        public Day11() : this(1_000_000) { }

        public override object SolvePart1(string[] input) => CalculateSumOfDistances(ToPositions(input));
        public override object SolvePart2(string[] input) => CalculateSumOfDistances(ToPositions(input, _timesLarger - 1));

        // See https://github.com/maksverver/AdventOfCode/blob/master/2023/day11/snel-met-uitleg.py for a solution with explanation (NL)
        private static long CalculateSumOfDistances(List<Coords> positions)
        {
            var sum = 0L;
            for (int a = 0; a < positions.Count; a++)
            {
                for (int b = a + 1; b < positions.Count; b++)
                {
                    sum += positions[a].DistanceTo(positions[b]);
                }
            }
            return sum;
        }

        private static List<Coords> ToPositions(string[] lines, int timesLarger = 1)
        {
            var positions = new List<Coords>();
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        positions.Add(new(x, y));
                    }
                }
            }
            var count = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                if (lines[y].All(data => data == '.'))
                {
                    var offset = new Coords(0, timesLarger);
                    for (int i = 0; i < positions.Count; i++)
                    {
                        if (positions[i].Y > y + count * timesLarger)
                        {
                            positions[i] += offset;
                        }
                    }
                    count++;
                }
            }
            count = 0;
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines.All(row => row[x] == '.'))
                {
                    var offset = new Coords(timesLarger, 0);
                    for (int i = 0; i < positions.Count; i++)
                    {
                        if (positions[i].X > x + count * timesLarger)
                        {
                            positions[i] += offset;
                        }
                    }
                    count++;
                }
            }
            return positions;
        }
    }
}
