using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2015.Solvers
{
    public class Day03 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var current = new Coords(0, 0);
            var houses = new HashSet<Coords> { current };
            foreach (var direction in input)
            {
                houses.Add(current = Move(current, direction));
            }
            return houses.Count;
        }

        public override object SolvePart2(string input)
        {
            var current = new Coords(0, 0);
            var houses = new HashSet<Coords> { current };
            for (int i = 0; i < input.Length; i+=2)
            {
                houses.Add(current = Move(current, input[i]));
            }
            current = new Coords(0, 0);
            for (int i = 1; i < input.Length; i += 2)
            {
                houses.Add(current = Move(current, input[i]));
            }
            return houses.Count;
        }

        private static Coords Move(Coords current, char direction)
        {
            return direction switch
            {
                '^' => current.Up,
                '>' => current.Right,
                'v' => current.Down,
                '<' => current.Left,
                _ => throw new InvalidOperationException($"Unknown direction: {direction}")
            };
        }
    }
}
