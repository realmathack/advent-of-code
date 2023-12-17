namespace AdventOfCode.Y2016.Solvers
{
    public class Day01 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var current = new Coords(0, 0);
            var direction = Direction.North;
            foreach (var instruction in input.Split(", "))
            {
                direction = (instruction[0] == 'R') ? (Direction)(((int)direction + 1) % 4) : (Direction)((4 + (int)direction - 1) % 4);
                var offset = ToOffset(direction);
                for (int i = 0; i < int.Parse(instruction[1..]); i++)
                {
                    current += offset;
                }
            }
            return Math.Abs(current.X) + Math.Abs(current.Y);
        }

        public override object SolvePart2(string input)
        {
            var current = new Coords(0, 0);
            var direction = Direction.North;
            var visited = new HashSet<Coords>() { current };
            foreach (var instruction in input.Split(", "))
            {
                direction = (instruction[0] == 'R') ? (Direction)(((int)direction + 1) % 4) : (Direction)((4 + (int)direction - 1) % 4);
                var offset = ToOffset(direction);
                for (int i = 0; i < int.Parse(instruction[1..]); i++)
                {
                    current += offset;
                    if (!visited.Add(current))
                    {
                        return Math.Abs(current.X) + Math.Abs(current.Y);
                    }
                }
            }
            return 0;
        }

        private static Coords ToOffset(Direction direction)
        {
            return direction switch
            {
                Direction.North => Coords.OffsetUp,
                Direction.East => Coords.OffsetRight,
                Direction.South => Coords.OffsetDown,
                Direction.West => Coords.OffsetLeft,
                _ => throw new ArgumentException($"Unknown value: {direction}", nameof(direction))
            };
        }

        private enum Direction { North, East, South, West }
    }
}
