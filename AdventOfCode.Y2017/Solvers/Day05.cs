namespace AdventOfCode.Y2017.Solvers
{
    public class Day05 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var steps = 0;
            var current = 0;
            var instructions = input.Select(int.Parse).ToArray();
            while (current >= 0 && current < instructions.Length)
            {
                current += instructions[current]++;
                steps++;
            }
            return steps;
        }

        public override object SolvePart2(string[] input)
        {
            var steps = 0;
            var current = 0;
            var instructions = input.Select(int.Parse).ToArray();
            while (current >= 0 && current < instructions.Length)
            {
                var value = instructions[current];
                instructions[current] = instructions[current] + ((instructions[current] >= 3) ? -1 : 1);
                current += value;
                steps++;
            }
            return steps;
        }
    }
}
