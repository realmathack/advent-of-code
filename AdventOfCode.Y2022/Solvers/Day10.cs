using System.Text;

namespace AdventOfCode.Y2022.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var registerX = 1;
            var signalStrength = 0;
            var instructions = ToAddInstructions(input);
            for (int i = 1; i <= 220; i++)
            {
                if (i % 40 == 20)
                {
                    signalStrength += registerX * i;
                }
                if (instructions.TryGetValue(i - 1, out var addition))
                {
                    registerX += addition;
                }
            }
            return signalStrength;
        }

        public override object SolvePart2(string[] input)
        {
            var registerX = 1;
            var screen = new StringBuilder();
            var instructions = ToAddInstructions(input);
            for (int i = 1; i <= 240; i++)
            {
                if (i % 40 == 1)
                {
                    screen.AppendLine();
                }
                screen.Append(IsLit(registerX, (i - 1) % 40) ? '#' : '.');
                if (instructions.TryGetValue(i - 1, out var addition))
                {
                    registerX += addition;
                }
            }
            return screen.ToString();
        }

        private static Dictionary<int, int> ToAddInstructions(string[] lines)
        {
            var instructions = new Dictionary<int, int>();
            var cycle = 1;
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    instructions.Add(cycle++, int.Parse(parts[1]));
                }
                cycle++;
            }
            return instructions;
        }

        private static bool IsLit(int registerX, int position) => position >= registerX - 1 && position <= registerX + 1;
    }
}
