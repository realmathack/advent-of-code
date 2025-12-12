using System.Text.RegularExpressions;

namespace AdventOfCode.Y2025.Solvers
{
    public partial class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var machines = ToMachines(input);
            return machines.Sum(FindFewestButtonPresses);
        }

        public override object SolvePart2(string[] input)
        {
            // TODO:
            // https://old.reddit.com/r/adventofcode/comments/1pk87hl/2025_day_10_part_2_bifurcate_your_way_to_victory/
            return null!;
        }

        private static int FindFewestButtonPresses(Machine machine)
        {
            var queue = new Queue<ButtonPressed>();
            queue.Enqueue(new(0, []));
            while (queue.TryDequeue(out var current))
            {
                if (machine.Indicators == current.Lights)
                {
                    return current.Pressed.Count;
                }
                for (int i = 0; i < machine.Buttons.Count; i++)
                {
                    if (current.Pressed.Contains(i)) // Pressing buttons more than once is not useful (0 ^ 1 ^ 1 == 0)
                    {
                        continue;
                    }
                    var afterPress = PressButtons(current.Lights, machine.Buttons[i], machine.Joltages.Length - 1);
                    var pressed = new HashSet<int>(current.Pressed) { i };
                    queue.Enqueue(new(afterPress, pressed));
                }
            }
            return int.MaxValue;
        }

        private static int PressButtons(int lights, int[] buttons, int lightCount)
        {
            foreach (var button in buttons)
            {
                lights ^= 1 << (lightCount - button);
            }
            return lights;
        }

        private static Machine[] ToMachines(string[] lines)
        {
            var machines = new Machine[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var match = MachineRegex().Match(lines[i]);
                var indicators = Convert.ToInt32(match.Groups[1].Value.Replace('#', '1').Replace('.', '0'), 2);
                var buttons = match.Groups[2].Value.Split(' ').Select(group => group[1..^1].Split(',').Select(int.Parse).ToArray()).ToList();
                var joltages = match.Groups[3].Value.Split(',').Select(int.Parse).ToArray();
                machines[i] = new(indicators, buttons, joltages);
            }
            return machines;
        }

        [GeneratedRegex(@"\[([.#]+)\] (.+) \{([\d,]+)\}")]
        private static partial Regex MachineRegex();

        private record class Machine(int Indicators, List<int[]> Buttons, int[] Joltages);
        private record class ButtonPressed(int Lights, HashSet<int> Pressed);
    }
}
