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
                var (_, result) = new IntCodeInterpreter(input).ExecuteProgram(permutation[0], 0);
                (_, result) = new IntCodeInterpreter(input).ExecuteProgram([permutation[1], .. result]);
                (_, result) = new IntCodeInterpreter(input).ExecuteProgram([permutation[2], .. result]);
                (_, result) = new IntCodeInterpreter(input).ExecuteProgram([permutation[3], .. result]);
                (_, result) = new IntCodeInterpreter(input).ExecuteProgram([permutation[4], .. result]);
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
                var amplifierA = new IntCodeInterpreter(input);
                amplifierA.Inputs.Enqueue(permutation[0]);
                var amplifierB = new IntCodeInterpreter(input);
                amplifierB.Inputs.Enqueue(permutation[1]);
                var amplifierC = new IntCodeInterpreter(input);
                amplifierC.Inputs.Enqueue(permutation[2]);
                var amplifierD = new IntCodeInterpreter(input);
                amplifierD.Inputs.Enqueue(permutation[3]);
                var amplifierE = new IntCodeInterpreter(input);
                amplifierE.Inputs.Enqueue(permutation[4]);
                var halted = false;
                var result = new int[1] { 0 };
                while (!halted)
                {
                    (halted, result) = amplifierA.ExecuteProgram(result);
                    (halted, result) = amplifierB.ExecuteProgram(result);
                    (halted, result) = amplifierC.ExecuteProgram(result);
                    (halted, result) = amplifierD.ExecuteProgram(result);
                    (halted, result) = amplifierE.ExecuteProgram(result);
                }
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

            public (bool Halted, int[] Outputs) ExecuteProgram(params int[] inputs)
            {
                foreach (var input in inputs)
                {
                    Inputs.Enqueue(input);
                }
                var halted = false;
                var outputs = new List<int>();
                while (true)
                {
                    var instruction = _memory[_pointer].ToString().PadLeft(5, '0');
                    var opCode = instruction[4];
                    if (instruction[3] == '9' && opCode == '9')
                    {
                        halted = true;
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
                        if (!Inputs.TryDequeue(out var input))
                        {
                            break;
                        }
                        _memory[_memory[_pointer + 1]] = input;
                        _pointer += 2;
                    }
                    else if (opCode == '4')
                    {
                        outputs.Add(GetParameter(_pointer + 1, instruction[2]));
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
                return (halted, [.. outputs]);
            }

            private int GetParameter(int pointer, char mode) => mode == '1' ? _memory[pointer] : _memory[_memory[pointer]];
        }
    }
}
