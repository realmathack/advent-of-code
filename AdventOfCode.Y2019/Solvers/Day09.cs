namespace AdventOfCode.Y2019.Solvers
{
    public class Day09 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var interpreter = new IntCodeInterpreter(input);
            return interpreter.ExecuteProgram(1L)[^1];
        }

        public override object SolvePart2(string input)
        {
            var interpreter = new IntCodeInterpreter(input);
            return interpreter.ExecuteProgram(2L)[^1];
        }

        private class IntCodeInterpreter(string intCode)
        {
            private readonly Dictionary<long, long> _memory = intCode.Split(',').Select((code, i) => (Index: (long)i, Code: long.Parse(code))).ToDictionary();
            private long _relativeBase = 0L;

            public Queue<long> Inputs { get; } = [];

            public List<long> ExecuteProgram(params long[] inputs)
            {
                foreach (var input in inputs)
                {
                    Inputs.Enqueue(input);
                }
                var outputs = new List<long>();
                var pointer = 0L;
                while (true)
                {
                    var instruction = GetMemory(pointer).ToString().PadLeft(5, '0');
                    var opCode = instruction[4];
                    if (instruction[3] == '9' && opCode == '9')
                    {
                        break;
                    }
                    if (opCode == '1' || opCode == '2')
                    {
                        var value1 = GetMemory(pointer + 1, instruction[2]);
                        var value2 = GetMemory(pointer + 2, instruction[1]);
                        var result = (opCode == '1') ? value1 + value2 : value1 * value2;
                        SetMemory(pointer + 3, instruction[0], result);
                        pointer += 4;
                    }
                    else if (opCode == '3')
                    {
                        SetMemory(pointer + 1, instruction[2], Inputs.Dequeue());
                        pointer += 2;
                    }
                    else if (opCode == '4')
                    {
                        outputs.Add(GetMemory(pointer + 1, instruction[2]));
                        pointer += 2;
                    }
                    else if (opCode == '5' || opCode == '6')
                    {
                        var value1 = GetMemory(pointer + 1, instruction[2]);
                        var result = (opCode == '5') ? value1 != 0 : value1 == 0;
                        pointer = result ? GetMemory(pointer + 2, instruction[1]) : pointer + 3;
                    }
                    else if (opCode == '7' || opCode == '8')
                    {
                        var value1 = GetMemory(pointer + 1, instruction[2]);
                        var value2 = GetMemory(pointer + 2, instruction[1]);
                        var result = (opCode == '7') ? value1 < value2 : value1 == value2;
                        SetMemory(pointer + 3, instruction[0], result ? 1 : 0);
                        pointer += 4;
                    }
                    else if (opCode == '9')
                    {
                        _relativeBase += GetMemory(pointer + 1, instruction[2]);
                        pointer += 2;
                    }
                }
                return outputs;
            }

            private long GetMemory(long pointer) => _memory.TryGetValue(pointer, out var value) ? value : 0L;
            private long GetMemory(long pointer, char mode) => mode switch
            {
                '0' => GetMemory(GetMemory(pointer)),
                '1' => GetMemory(pointer),
                '2' => GetMemory(_relativeBase + GetMemory(pointer)),
                _ => throw new InvalidOperationException($"Unknown read mode: {mode}")
            };

            private void SetMemory(long pointer, char mode, long value)
            {
                switch (mode)
                {
                    case '0':
                        _memory[GetMemory(pointer)] = value;
                        break;
                    case '2':
                        _memory[_relativeBase + GetMemory(pointer)] = value;
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown write mode: {mode}");
                }
            }
        }
    }
}
