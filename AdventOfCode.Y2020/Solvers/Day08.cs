namespace AdventOfCode.Y2020.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => RunProgram(input).Accumulator;

        public override object SolvePart2(string[] input)
        {
            var indexes = input
                .Select((line, index) => (Instruction: line, Index: index))
                .Where(line => line.Instruction.StartsWith("nop") || line.Instruction.StartsWith("jmp"))
                .Select(line => line.Index)
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

        private static (bool Succes, int Accumulator) RunProgram(string[] lines)
        {
            var visited = new HashSet<int>();
            var accumulator = 0;
            var pc = 0;
            while (visited.Add(pc))
            {
                if (pc == lines.Length)
                {
                    return (true, accumulator);
                }
                if (pc > lines.Length)
                {
                    return (false, accumulator);
                }
                switch (lines[pc][..3])
                {
                    case "acc":
                        accumulator += int.Parse(lines[pc][4..]);
                        pc++;
                        break;
                    case "jmp":
                        pc += int.Parse(lines[pc][4..]);
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
