namespace AdventOfCode.Y2021.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var current = new Coords(0, 0);
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                var value = int.Parse(parts[1]);
                current += parts[0] switch
                {
                    "forward" => new Coords(value, 0),
                    "down" => new Coords(0, value),
                    "up" => new Coords(0, -value),
                    _ => throw new InvalidOperationException($"Unknown command {parts[0]}")
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
                var parts = line.Split(' ');
                var value = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        current += new Coords(value, value * aim);
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }
            return current.X * current.Y;
        }
    }
}
