namespace AdventOfCode.Y2023.Solvers
{
    public class Day08 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var instructions = input[0];
            var nodes = input[1].SplitIntoLines().Select(x => new Node(x[0..3], x[7..10], x[12..15])).ToDictionary(x => x.Name);
            var current = nodes["AAA"];
            var steps = 0;
            var index = 0;
            while (current.Name != "ZZZ")
            {
                current = nodes[(instructions[index] == 'L') ? current.Left : current.Right];
                index = ++index % instructions.Length;
                steps++;
            }
            return steps;
        }

        public override object SolvePart2(string[] input)
        {
            var instructions = input[0];
            var nodes = input[1].SplitIntoLines().Select(x => new Node(x[0..3], x[7..10], x[12..15])).ToDictionary(x => x.Name);
            var current = nodes.Where(x => x.Key.EndsWith('A')).Select(x => x.Value).ToArray();
            var intervals = new Dictionary<int, long>();
            var steps = 0L;
            var index = 0;
            while (intervals.Count != current.Length)
            {
                for (int i = 0; i < current.Length; i++)
                {
                    if (current[i].Name.EndsWith('Z') && !intervals.ContainsKey(i))
                    {
                        intervals[i] = steps;
                    }
                    current[i] = nodes[(instructions[index] == 'L') ? current[i].Left : current[i].Right];
                }
                index = ++index % instructions.Length;
                steps++;
            }
            return NumberTheory.LeastCommonMultiple([.. intervals.Values]);
        }

        private record class Node(string Name, string Left, string Right);
    }
}
