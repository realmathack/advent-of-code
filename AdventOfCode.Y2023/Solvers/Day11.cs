namespace AdventOfCode.Y2023.Solvers
{
    public class Day11(int _timesLarger) : SolverWithLines
    {
        public Day11() : this(1000000) { }

        public override object SolvePart1(string[] input) => CalculateSumOfDistances(ToCoords(input));
        public override object SolvePart2(string[] input) => CalculateSumOfDistances(ToCoords(input, _timesLarger - 1));

        // See https://github.com/maksverver/AdventOfCode/blob/master/2023/day11/snel-met-uitleg.py for a solution with explanation (NL)
        private static long CalculateSumOfDistances(List<Coords> coords)
        {
            var sum = 0L;
            for (int a = 0; a < coords.Count; a++)
            {
                for (int b = a + 1; b < coords.Count; b++)
                {
                    sum += coords[a].DistanceTo(coords[b]);
                }
            }
            return sum;
        }

        private static List<Coords> ToCoords(string[] input, int timesLarger = 1)
        {
            var coords = new List<Coords>();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        coords.Add(new(x, y));
                    }
                }
            }
            var count = 0;
            for (int y = 0; y < input.Length; y++)
            {
                if (input[y].All(data => data == '.'))
                {
                    var offset = new Coords(0, timesLarger);
                    for (int i = 0; i < coords.Count; i++)
                    {
                        if (coords[i].Y > y + count * timesLarger)
                        {
                            coords[i] += offset;
                        }
                    }
                    count++;
                }
            }
            count = 0;
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input.All(row => row[x] == '.'))
                {
                    var offset = new Coords(timesLarger, 0);
                    for (int i = 0; i < coords.Count; i++)
                    {
                        if (coords[i].X > x + count * timesLarger)
                        {
                            coords[i] += offset;
                        }
                    }
                    count++;
                }
            }
            return coords;
        }
    }
}
