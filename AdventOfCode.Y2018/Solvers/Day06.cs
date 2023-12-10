namespace AdventOfCode.Y2018.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            // TODO: Implement
            // var coords = ToCoords(input);
            return null!;
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Implement
            return null!;
        }

        private static List<Coords> ToCoords(string[] input)
        {
            var coords = new List<Coords>();
            foreach (var line in input)
            {
                var parts = line.Split(", ");
                coords.Add(new(int.Parse(parts[0]), int.Parse(parts[1])));
            }
            return coords;
        }
    }
}
