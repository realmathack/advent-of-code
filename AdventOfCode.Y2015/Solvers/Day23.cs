namespace AdventOfCode.Y2015.Solvers
{
    public class Day23 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ExecuteInstructions(input, new() { { 'a', 0 }, { 'b', 0 } });
        public override object SolvePart2(string[] input) => ExecuteInstructions(input, new() { { 'a', 1 }, { 'b', 0 } });

        private static int ExecuteInstructions(string[] lines, Dictionary<char, int> registers)
        {
            var pc = 0;
            while (pc >= 0 && pc < lines.Length)
            {
                switch (lines[pc][..3])
                {
                    case "hlf":
                        registers[lines[pc][4]] /= 2;
                        break;
                    case "tpl":
                        registers[lines[pc][4]] *= 3;
                        break;
                    case "inc":
                        registers[lines[pc][4]]++;
                        break;
                    case "jmp":
                        pc += int.Parse(lines[pc][4..]);
                        continue;
                    case "jie":
                        if (registers[lines[pc][4]] % 2 == 0)
                        {
                            pc += int.Parse(lines[pc][6..]);
                            continue;
                        }
                        break;
                    case "jio":
                        if (registers[lines[pc][4]] == 1)
                        {
                            pc += int.Parse(lines[pc][6..]);
                            continue;
                        }
                        break;
                    default:
                        throw new ArgumentException($"Unknown opcode {lines[pc][..2]}!");
                }
                pc++;
            }
            return registers['b'];
        }
    }
}
