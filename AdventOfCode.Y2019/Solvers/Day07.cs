namespace AdventOfCode.Y2019.Solvers
{
    public class Day07 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var highest = 0;
            var phaseSettings = new int[] { 0, 1, 2, 3, 4 };
            foreach (var permutation in phaseSettings.Permutations())
            {
                var result = new IntCodeInterpreter(input).ExecuteProgram(permutation[0], 0);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[1], .. result]);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[2], .. result]);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[3], .. result]);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[4], .. result]);
                if (result[0] > highest)
                {
                    highest = result[0];
                }
            }
            return highest;
        }

        public override object SolvePart2(string input)
        {
            var highest = 0;
            var phaseSettings = new int[] { 5, 6, 7, 8, 9 };
            foreach (var permutation in phaseSettings.Permutations())
            {
                // TODO: Implement way to interrupt execution in Interpreter
                var result = new IntCodeInterpreter(input).ExecuteProgram(permutation[0], 0);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[1], .. result]);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[2], .. result]);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[3], .. result]);
                result = new IntCodeInterpreter(input).ExecuteProgram([permutation[4], .. result]);
                if (result[0] > highest)
                {
                    highest = result[0];
                }
            }
            return highest;
        }

        private class IntCodeInterpreter(string intCode)
        {
            private readonly int[] _memory = intCode.Split(',').Select(int.Parse).ToArray();
            private int _pointer = 0;

            public Queue<int> Inputs { get; } = [];
            public List<int> Outputs { get; } = [];

            public List<int> ExecuteProgram(params int[] inputs)
            {
                foreach (var input in inputs)
                {
                    Inputs.Enqueue(input);
                }
                Outputs.Clear();
                while (true)
                {
                    var instruction = _memory[_pointer].ToString().PadLeft(5, '0');
                    var opCode = instruction[4];
                    if (instruction[3] == '9' && opCode == '9')
                    {
                        break;
                    }
                    if (opCode == '1' || opCode == '2')
                    {
                        var value1 = GetParameter(_pointer + 1, instruction[2]);
                        var value2 = GetParameter(_pointer + 2, instruction[1]);
                        var result = (opCode == '1') ? value1 + value2 : value1 * value2;
                        _memory[_memory[_pointer + 3]] = result;
                        _pointer += 4;
                    }
                    else if (opCode == '3')
                    {
                        _memory[_memory[_pointer + 1]] = Inputs.Dequeue();
                        _pointer += 2;
                    }
                    else if (opCode == '4')
                    {
                        Outputs.Add(GetParameter(_pointer + 1, instruction[2]));
                        _pointer += 2;
                    }
                    else if (opCode == '5' || opCode == '6')
                    {
                        var value1 = GetParameter(_pointer + 1, instruction[2]);
                        var result = (opCode == '5') ? value1 != 0 : value1 == 0;
                        _pointer = result ? GetParameter(_pointer + 2, instruction[1]) : _pointer + 3;
                    }
                    else if (opCode == '7' || opCode == '8')
                    {
                        var value1 = GetParameter(_pointer + 1, instruction[2]);
                        var value2 = GetParameter(_pointer + 2, instruction[1]);
                        var result = (opCode == '7') ? value1 < value2 : value1 == value2;
                        _memory[_memory[_pointer + 3]] = result ? 1 : 0;
                        _pointer += 4;
                    }
                }
                return Outputs;
            }

            private int GetParameter(int pointer, char mode) => mode == '1' ? _memory[pointer] : _memory[_memory[pointer]];
        }
    }
}
