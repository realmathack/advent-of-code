namespace AdventOfCode.Y2021.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var current = new Coords(0, 0);
            foreach (var line in input)
            {
                var (direction, value) = line.SplitInTwo(' ');
                var number = int.Parse(value);
                current += direction switch
                {
                    "forward" => new Coords(number, 0),
                    "down" => new Coords(0, number),
                    "up" => new Coords(0, -number),
                    _ => throw new InvalidOperationException($"Unknown command {direction}")
                };
            }
            return current.X * current.Y;
        }

        public override object SolvePart2(string[] input)
        {
            var current = new Coords(0, 0);
            var aim = 0;
            foreach (var line in input)
            {
                var (direction, value) = line.SplitInTwo(' ');
                var number = int.Parse(value);
                switch (direction)
                {
                    case "forward":
                        current += new Coords(number, number * aim);
                        break;
                    case "down":
                        aim += number;
                        break;
                    case "up":
                        aim -= number;
                        break;
                }
            }
            return current.X * current.Y;
        }
    }
}
