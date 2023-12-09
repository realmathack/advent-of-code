namespace AdventOfCode.Y2019.Solvers
{
    public class Day05 : SolverWithText
    {
        public override object SolvePart1(string input) => ExecuteProgram(input, 1);

        public override object SolvePart2(string input) => ExecuteProgram(input, 5);

        private static int ExecuteProgram(string input, int givenInput)
        {
            var program = input.Split(',').Select(int.Parse).ToArray();
            var output = new List<int>();
            var pointer = 0;
            while (true)
            {
                var opCode = $"    {program[pointer]}";
                if (opCode[^2..] == "99")
                {
                    break;
                }
                if (opCode[^1] == '1' || opCode[^1] == '2')
                {
                    var value1 = GetParameter(program, pointer + 1, opCode[^3]);
                    var value2 = GetParameter(program, pointer + 2, opCode[^4]);
                    var result = (opCode[^1] == '1') ? value1 + value2 : value1 * value2;
                    program[program[pointer + 3]] = result;
                    pointer += 4;
                }
                else if (opCode[^1] == '3')
                {
                    program[program[pointer + 1]] = givenInput;
                    pointer += 2;
                }
                else if (opCode[^1] == '4')
                {
                    output.Add(GetParameter(program, pointer + 1, opCode[^3]));
                    pointer += 2;
                }
                else if (opCode[^1] == '5' || opCode[^1] == '6')
                {
                    var value1 = GetParameter(program, pointer + 1, opCode[^3]);
                    var result = (opCode[^1] == '5') ? value1 != 0 : value1 == 0;
                    pointer = result ? GetParameter(program, pointer + 2, opCode[^4]) : pointer + 3;
                }
                else if (opCode[^1] == '7' || opCode[^1] == '8')
                {
                    var value1 = GetParameter(program, pointer + 1, opCode[^3]);
                    var value2 = GetParameter(program, pointer + 2, opCode[^4]);
                    var result = (opCode[^1] == '7') ? value1 < value2 : value1 == value2;
                    program[program[pointer + 3]] = result ? 1 : 0;
                    pointer += 4;
                }
            }
            return output[^1];
        }

        private static int GetParameter(int[] program, int pointer, char mode)
        {
            return mode == '1' ? program[pointer] : program[program[pointer]];
        }
    }
}
