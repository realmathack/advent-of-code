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
                var offset = FromDirection(direction);
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
                var offset = FromDirection(direction);
                for (int i = 0; i < int.Parse(instruction[1..]); i++)
                {
                    current += offset;
                    if (visited.Contains(current))
                    {
                        return Math.Abs(current.X) + Math.Abs(current.Y);
                    }
                    visited.Add(current);
                }
            }
            return 0;
        }

        private static Coords FromDirection(Direction direction)
        {
            return direction switch
            {
                Direction.North => new(0, -1),
                Direction.East => new(1, 0),
                Direction.South => new(0, 1),
                Direction.West => new(-1, 0),
                _ => throw new ArgumentException($"Unknown value: {direction}", nameof(direction))
            };
        }

        private enum Direction { North, East, South, West }
    }
}
