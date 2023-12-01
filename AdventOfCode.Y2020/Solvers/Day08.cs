namespace AdventOfCode.Y2020.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (_, accumulator) = RunProgram(input);
            return accumulator;
        }

        public override object SolvePart2(string[] input)
        {
            var indexes = input
                .Select((instruction, index) => new { instruction, index })
                .Where(x => x.instruction.StartsWith("nop") || x.instruction.StartsWith("jmp"))
                .Select(x => x.index)
                .ToList();
            foreach (var index in indexes)
            {
                var program = new List<string>(input);
                program[index] = string.Concat((program[index][..3] == "jmp") ? "nop" : "jmp", program[index][3..]);
                var (succes, accumulator) = RunProgram([.. program]);
                if (succes)
                {
                    return accumulator;
                }
            }
            return 0;
        }

        private static (bool, int) RunProgram(string[] input)
        {
            var visited = new HashSet<int>();
            var accumulator = 0;
            var pc = 0;
            while (visited.Add(pc))
            {
                if (pc == input.Length)
                {
                    return (true, accumulator);
                }
                if (pc > input.Length)
                {
                    return (false, accumulator);
                }
                switch (input[pc][..3])
                {
                    case "acc":
                        accumulator += int.Parse(input[pc][4..]);
                        pc++;
                        break;
                    case "jmp":
                        pc += int.Parse(input[pc][4..]);
                        break;
                    case "nop":
                        pc++;
                        break;
                }
            }
            return (false, accumulator);
        }
    }
}
