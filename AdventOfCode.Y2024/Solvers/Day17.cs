namespace AdventOfCode.Y2024.Solvers
{
    public class Day17 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => string.Join(',', new Computer(ToInstructionsAndRegisterA(input)).Execute());

        public override object SolvePart2(string[] input)
        {
            var (instructions, _) = ToInstructionsAndRegisterA(input);
            var computer = new Computer(instructions);
            // TODO: Reverse engineer to determine initialValue
            var initialValue = 0L;
            computer.Reset(initialValue);
            var result = computer.Execute();
            if (result.Count == instructions.Length && string.Join(',', result) == input[4])
            {
                return initialValue;
            }
            return -1L;
        }

        private static (int[] Instructions, long A) ToInstructionsAndRegisterA(string[] lines)
        {
            var pos = lines[4].IndexOf(' ');
            var instructions = lines[4][(pos + 1)..].Split(',').Select(int.Parse).ToArray();
            pos = lines[0].LastIndexOf(' ');
            var a = long.Parse(lines[0][(pos + 1)..]);
            return (instructions, a);
        }

        private class Computer(int[] instructions)
        {
            private readonly int[] _instructions = instructions;
            private long _a;
            private long _b;
            private long _c;

            public Computer((int[] Instructions, long A) input)
                : this(input.Instructions)
            {
                _a = input.A;
            }

            public void Reset(long a)
            {
                _a = a;
                _b = 0;
                _c = 0;
            }

            public List<long> Execute()
            {
                var output = new List<long>();
                for (int pointer = 0; pointer < _instructions.Length; pointer += 2)
                {
                    var operand = _instructions[pointer + 1];
                    switch (_instructions[pointer])
                    {
                        /* Rewrites:
                         *  x / Math.Pow(2, y)  ->  x >> y
                         *  x % 8               ->  x & 7
                         */
                        case 0:
                            _a >>= operand;
                            break;
                        case 1:
                            _b ^= operand;
                            break;
                        case 2:
                            _b = GetComboValue(operand) & 7;
                            break;
                        case 3:
                            pointer = (_a == 0) ? pointer : operand - 2;
                            break;
                        case 4:
                            _b ^= _c;
                            break;
                        case 5:
                            output.Add(GetComboValue(operand) & 7);
                            break;
                        case 6:
                            _b = _a >> operand;
                            break;
                        case 7:
                            _c = _a >> operand;
                            break;
                    }
                }
                return output;
            }

            private long GetComboValue(long operand) => operand switch
            {
                >= 0 and <= 3 => operand,
                4 => _a,
                5 => _b,
                6 => _c,
                7 => throw new ImpossibleException("Operand 7 should not be used"),
                _ => throw new ArgumentException($"Unknown operand: {operand}", nameof(operand))
            };
        }
    }
}
