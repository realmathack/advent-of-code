namespace AdventOfCode.Y2025.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToMachines(input).Sum(FindFewestButtonPressesIndicator);
        public override object SolvePart2(string[] input) => ToMachines(input).Sum(FindFewestButtonPressesJoltage);

        private static int FindFewestButtonPressesJoltage(Machine machine)
        {
            // TODO:
            // https://old.reddit.com/r/adventofcode/comments/1pk87hl/2025_day_10_part_2_bifurcate_your_way_to_victory/
            // https://aoc.winslowjosiah.com/solutions/2025/day/10/
            // -> https://github.com/WinslowJosiah/adventofcode/blob/main/solutions/2025/day10/solution.py
            return 0;
        }

        private static int FindFewestButtonPressesIndicator(Machine machine)
        {
            var distance = new Dictionary<int, int>() { [0] = 0 };
            var queue = new Queue<int>();
            queue.Enqueue(0);
            while (queue.TryDequeue(out var current))
            {
                foreach (var buttonMask in machine.Buttons)
                {
                    var afterPress = current ^ buttonMask;
                    if (distance.ContainsKey(afterPress))
                    {
                        continue;
                    }
                    distance[afterPress] = distance[current] + 1;
                    if (afterPress == machine.Indicators)
                    {
                        return distance[afterPress];
                    }
                    queue.Enqueue(afterPress);
                }
            }
            return int.MaxValue;
        }

        private static Machine[] ToMachines(string[] lines)
        {
            var machines = new Machine[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(' ');
                var indicators = Convert.ToInt32(parts[0][1..^1].Replace('#', '1').Replace('.', '0'), 2);
                var buttons = parts[1..^1].Select(group => ToButtonsMask(group[1..^1].Split(',').Select(int.Parse), parts[0].Length - 2 - 1)).ToArray();
                var joltages = parts[^1][1..^1].Split(',').Select(int.Parse).ToArray();
                machines[i] = new(indicators, buttons, joltages);
            }
            return machines;
        }

        private static int ToButtonsMask(IEnumerable<int> buttons, int maxLightIndex)
        {
            var mask = 0;
            foreach (var button in buttons)
            {
                mask ^= 1 << (maxLightIndex - button);
            }
            return mask;
        }

        private record class Machine(int Indicators, int[] Buttons, int[] Joltages);
    }
}
