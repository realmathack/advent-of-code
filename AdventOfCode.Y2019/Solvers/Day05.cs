namespace AdventOfCode.Y2019.Solvers
{
    public class Day05 : SolverWithText
    {
        public override object SolvePart1(string input) => ExecuteProgram(input, 1);
        public override object SolvePart2(string input) => ExecuteProgram(input, 5);

        private static int ExecuteProgram(string intCode, int givenInput)
        {
            var program = intCode.Split(',').Select(int.Parse).ToArray();
            var output = new List<int>();
            var pointer = 0;
            while (true)
            {
                var instruction = program[pointer].ToString().PadLeft(5, '0');
                var opCode = instruction[4];
                if (instruction[3] == '9' && opCode == '9')
                {
                    break;
                }
                if (opCode == '1' || opCode == '2')
                {
                    var value1 = GetParameter(program, pointer + 1, instruction[2]);
                    var value2 = GetParameter(program, pointer + 2, instruction[1]);
                    var result = (opCode == '1') ? value1 + value2 : value1 * value2;
                    program[program[pointer + 3]] = result;
                    pointer += 4;
                }
                else if (opCode == '3')
                {
                    program[program[pointer + 1]] = givenInput;
                    pointer += 2;
                }
                else if (opCode == '4')
                {
                    output.Add(GetParameter(program, pointer + 1, instruction[2]));
                    pointer += 2;
                }
                else if (opCode == '5' || opCode == '6')
                {
                    var value1 = GetParameter(program, pointer + 1, instruction[2]);
                    var result = (opCode == '5') ? value1 != 0 : value1 == 0;
                    pointer = result ? GetParameter(program, pointer + 2, instruction[1]) : pointer + 3;
                }
                else if (opCode == '7' || opCode == '8')
                {
                    var value1 = GetParameter(program, pointer + 1, instruction[2]);
                    var value2 = GetParameter(program, pointer + 2, instruction[1]);
                    var result = (opCode == '7') ? value1 < value2 : value1 == value2;
                    program[program[pointer + 3]] = result ? 1 : 0;
                    pointer += 4;
                }
            }
            return output[^1];
        }

        private static int GetParameter(int[] program, int pointer, char mode) => mode == '1' ? program[pointer] : program[program[pointer]];
    }
}
