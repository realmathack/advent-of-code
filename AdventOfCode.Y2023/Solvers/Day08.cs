namespace AdventOfCode.Y2023.Solvers
{
    public class Day08 : SolverWithSections
    {
        public override object SolvePart1(string[] input) => GetSteps(input, "AAA", "ZZZ")[0];

        public override object SolvePart2(string[] input) => NumberTheory.LeastCommonMultiple(GetSteps(input, "A", "Z"));

        private static long[] GetSteps(string[] input, string start, string end)
        {
            var instructions = input[0];
            var nodes = input[1].SplitIntoLines().Select(x => new Node(x[0..3], x[7..10], x[12..15])).ToDictionary(x => x.Name);
            var current = nodes.Where(x => x.Key.EndsWith(start)).Select(x => x.Value).ToArray();
            var steps = new long[current.Length];
            var index = 0;
            while (steps.Any(x => x == default))
            {
                for (int i = 0; i < current.Length; i++)
                {
                    if (current[i].Name.EndsWith(end) && steps[i] == default)
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
