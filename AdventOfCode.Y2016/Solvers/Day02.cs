namespace AdventOfCode.Y2016.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var keypad = new[]
            {
                ".....",
                ".123.",
                ".456.",
                ".789.",
                "....."
            };
            return GetCode(keypad, new Coords(2, 2), input);
        }

        public override object SolvePart2(string[] input)
        {
            var keypad = new[]
            {
                ".......",
                "...1...",
                "..234..",
                ".56789.",
                "..ABC..",
                "...D...",
                "......."
            };
            return GetCode(keypad, new Coords(1, 3), input);
        }

        private string GetCode(string[] keypad, Coords current, string[] lines)
        {
            var code = string.Empty;
            foreach (var line in lines)
            {
                foreach (var instruction in line)
                {
                    var newCoords = instruction switch
                    {
                        'L' => current.Left,
                        'U' => current.Up,
                        'R' => current.Right,
                        'D' => current.Down,
                        _ => throw new InvalidOperationException($"Unknown instruction {instruction}")
                    };
                    if (keypad[newCoords.Y][newCoords.X] != '.')
                    {
                        current = newCoords;
                    }
                }
                code += keypad[current.Y][current.X];
            }
            return code;
        }
    }
}
