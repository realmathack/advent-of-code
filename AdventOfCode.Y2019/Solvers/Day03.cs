
namespace AdventOfCode.Y2019.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var centralPort = new Coords(0, 0);
            var firstWire = GetWire(input[0], centralPort);
            var secondWire = GetWire(input[1], centralPort);
            var distances = firstWire.Intersect(secondWire).Select(x => x.DistanceTo(centralPort)).OrderBy(x => x);
            return distances.First();
        }

        public override object SolvePart2(string[] input)
        {
            var centralPort = new Coords(0, 0);
            var firstWire = GetWire(input[0], centralPort);
            var secondWire = GetWire(input[1], centralPort);
            var distances = firstWire.Intersect(secondWire).Select(x => 2 + firstWire.IndexOf(x) + secondWire.IndexOf(x)).OrderBy(x => x);
            return distances.First();
        }

        private static List<Coords> GetWire(string input, Coords current)
        {
            var wire = new List<Coords>();
            foreach (var direction in input.Split(','))
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
