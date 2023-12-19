namespace AdventOfCode.Y2019.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var centralPort = new Coords(0, 0);
            var firstWire = ToWire(input[0], centralPort);
            var secondWire = ToWire(input[1], centralPort);
            var distances = firstWire.Intersect(secondWire).Select(coord => coord.DistanceTo(centralPort)).OrderBy(distance => distance);
            return distances.First();
        }

        public override object SolvePart2(string[] input)
        {
            var centralPort = new Coords(0, 0);
            var firstWire = ToWire(input[0], centralPort);
            var secondWire = ToWire(input[1], centralPort);
            var distances = firstWire.Intersect(secondWire).Select(coord => 2 + firstWire.IndexOf(coord) + secondWire.IndexOf(coord)).OrderBy(distance => distance);
            return distances.First();
        }

        private static List<Coords> ToWire(string line, Coords current)
        {
            var wire = new List<Coords>();
            foreach (var direction in line.Split(','))
            {
                var offset = direction[0] switch
                {
                    'D' => Coords.OffsetDown,
                    'L' => Coords.OffsetLeft,
                    'R' => Coords.OffsetRight,
                    'U' => Coords.OffsetUp,
                    _ => throw new InvalidOperationException($"Unknown direction {direction[0]}!")
                };
                for (int i = 0; i < int.Parse(direction[1..]); i++)
                {
                    wire.Add(current += offset);
                }
            }
            return wire;
        }
    }
}
