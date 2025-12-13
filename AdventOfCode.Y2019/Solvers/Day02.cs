namespace AdventOfCode.Y2019.Solvers
{
    public class Day02(bool restore1202State) : SolverWithText
    {
        public Day02() : this(true) { }

        public override object SolvePart1(string input) => restore1202State ? ExecuteProgram(input, 12, 2) : ExecuteProgram(input);

        public override object SolvePart2(string input)
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    if (ExecuteProgram(input, noun, verb) == 19690720)
                    {
                        return 100 * noun + verb;
                    }
                }
            }
            return 0;
        }

        private static int ExecuteProgram(string intCode, int? noun = null, int? verb = null)
        {
            var program = intCode.Split(',').Select(int.Parse).ToArray();
            if (noun.HasValue && verb.HasValue)
            {
                program[1] = noun.Value;
                program[2] = verb.Value;
            }
            var pointer = 0;
            while (true)
            {
                if (program[pointer] == 99)
                {
                    break;
                }
                var value1 = program[program[pointer + 1]];
                var value2 = program[program[pointer + 2]];
                var result = program[pointer] switch
                {
                    1 => value1 + value2,
                    2 => value1 * value2,
                    _ => throw new ArgumentException($"Unknown opcode {program[pointer]}")
                };
                program[program[pointer + 3]] = result;
                pointer += 4;
            }
            return program[0];
        }
    }
}
