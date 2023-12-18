namespace AdventOfCode.Y2023.Solvers
{
    public class Day18 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var trenches = ToTrenches(input);
            var current = new Coords(0, 0);
            var vertices = new List<Coords>() { current };
            foreach (var trench in trenches)
            {
                var offset = trench.Direction switch
                {
                    'D' => Coords.OffsetDown,
                    'L' => Coords.OffsetLeft,
                    'R' => Coords.OffsetRight,
                    'U' => Coords.OffsetUp,
                    _ => throw new InvalidOperationException($"Unknown direction {trench.Direction}!")
                };
                vertices.Add(current += offset * trench.Length);
            }
            return CalculateArea(vertices);
        }

        public override object SolvePart2(string[] input)
        {
            var trenches = ToTrenches(input);
            var current = new Coords(0, 0);
            var vertices = new List<Coords>() { current };
            foreach (var trench in trenches)
            {
                var offset = trench.ColorCode[^1] switch
                {
                    '0' => Coords.OffsetRight,
                    '1' => Coords.OffsetDown,
                    '2' => Coords.OffsetLeft,
                    '3' => Coords.OffsetUp,
                    _ => throw new InvalidOperationException($"Unknown direction {trench.Direction}!")
                };
                var length = Convert.ToInt32(trench.ColorCode[..^1], 16);
                vertices.Add(current += offset * length);
            }
            return CalculateArea(vertices);
        }

        private static long CalculateArea(List<Coords> vertices)
        {
            var sum = 0L;
            for (int i = 1; i < vertices.Count; i++)
            {
                // https://en.wikipedia.org/wiki/Shoelace_formula
                sum += (long)vertices[i - 1].X * vertices[i].Y - (long)vertices[i].X * vertices[i - 1].Y;
            }
            sum = Math.Abs(sum);
            for (int i = 1; i < vertices.Count; i++)
            {
                sum += vertices[i - 1].DistanceTo(vertices[i]);
            }
            // https://en.wikipedia.org/wiki/Pick%27s_theorem
            // sum = 2A, b = 0      we want to know i:
            // A = i + (b / 2) - 1  can be written as:
            // i = A - (b / 2) + 1
            return sum / 2L + 1L;
        }

        private static List<Trench> ToTrenches(string[] input)
        {
            var trenches = new List<Trench>();
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                trenches.Add(new(parts[0][0], int.Parse(parts[1]), parts[2][2..^1]));
            }
            return trenches;
        }

        private record class Trench(char Direction, int Length, string ColorCode);
    }
}
