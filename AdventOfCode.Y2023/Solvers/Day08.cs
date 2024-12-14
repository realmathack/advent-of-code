namespace AdventOfCode.Y2023.Solvers
{
    public class Day08 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input) => FindSteps(input, "AAA", "ZZZ")[0];
        public override object SolvePart2(string[] input) => NumberTheory.LCM(FindSteps(input, "A", "Z"));

        private static long[] FindSteps(string[] lineGroups, string start, string goal)
        {
            var instructions = lineGroups[0];
            var nodes = lineGroups[1].SplitIntoLines().Select(line => new Node(line[0..3], line[7..10], line[12..15])).ToDictionary(node => node.Name);
            var current = nodes.Where(node => node.Key.EndsWith(start)).Select(node => node.Value).ToArray();
            var steps = new long[current.Length];
            var index = 0;
            while (steps.Any(step => step == default))
            {
                for (int i = 0; i < current.Length; i++)
                {
                    if (current[i].Name.EndsWith(goal) && steps[i] == default)
                    {
                        steps[i] = index;
                    }
                    current[i] = nodes[(instructions[index % instructions.Length] == 'L') ? current[i].Left : current[i].Right];
                }
                index++;
            }
            return steps;
        }

        private record class Node(string Name, string Left, string Right);
    }
}
