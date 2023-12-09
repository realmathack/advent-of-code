namespace AdventOfCode.Y2017.Solvers
{
    public class Day06 : SolverWithText
    {
        public override object SolvePart1(string input) => Solve(input);

        public override object SolvePart2(string input) => Solve(input, true);

        private static int Solve(string input, bool returnLoopSize = false)
        {
            var memory = input.Split('\t').Select(int.Parse).ToArray();
            var seen = new Dictionary<string, int>();
            var cycle = 0;
            while (!seen.ContainsKey(string.Join(',', memory)))
            {
                seen.Add(string.Join(',', memory), cycle++);
                var highest = memory.Max();
                int current = -1;
                for (int i = 0; i < memory.Length; i++)
                {
                    if (memory[i] == highest)
                    {
                        current = i;
                        break;
                    }
                }
                memory[current] = 0;
                var equalDistribution = highest / memory.Length;
                for (int i = 0; i < memory.Length; i++)
                {
                    memory[i] += equalDistribution;
                }
                for (int i = 0; i < highest % memory.Length; i++)
                {
                    memory[(current + 1 + i) % memory.Length]++;
                }
            }
            return returnLoopSize ? cycle - seen[string.Join(',', memory)] : seen.Count;
        }
    }
}
