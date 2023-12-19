namespace AdventOfCode.Y2015.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ExecuteInstructions(ToInstructions(input));

        public override object SolvePart2(string[] input)
        {
            var instructions = ToInstructions(input);
            var value = ExecuteInstructions(instructions);
            return ExecuteInstructions(instructions, value);
        }

        private static Dictionary<string, Instruction> ToInstructions(string[] lines)
        {
            var instructions = new Dictionary<string, Instruction>(lines.Length);
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length == 3)
                {
                    instructions.Add(parts[2], new(Operator.Assignment, [parts[0]]));
                    continue;
                }
                if (parts.Length == 4)
                {
                    instructions.Add(parts[3], new(Operator.Not, [parts[1]]));
                    continue;
                }
                var op = parts[1] switch
                {
                    "AND" => Operator.And,
                    "OR" => Operator.Or,
                    "LSHIFT" => Operator.LeftShift,
                    "RSHIFT" => Operator.RightShift,
                    _ => throw new InvalidOperationException($"Unknown operator: {parts[1]}")
                };
                instructions.Add(parts[4], new(op, [parts[0], parts[2]]));
            }
            return instructions;
        }

        private static ushort ExecuteInstructions(Dictionary<string, Instruction> instructions, ushort? setWireB = null)
        {
            var current = new Stack<string>();
            current.Push("a");
            var wires = new Dictionary<string, ushort>();
            if (setWireB.HasValue)
            {
                wires.Add("b", setWireB.Value);
            }
            while (current.Count > 0)
            {
                if (wires.ContainsKey(current.Peek()))
                {
                    current.Pop();
                    continue;
                }
                var instruction = instructions[current.Peek()];
                if (instruction.Operator == Operator.Assignment)
                {
                    if (ushort.TryParse(instruction.Operands[0], out var value) || wires.TryGetValue(instruction.Operands[0], out value))
                    {
                        wires.Add(current.Peek(), value);
                        continue;
                    }
                    current.Push(instruction.Operands[0]);
                }
                else if (instruction.Operator == Operator.Not)
                {
                    if (wires.TryGetValue(instruction.Operands[0], out var value))
                    {
                        wires.Add(current.Peek(), (ushort)~value);
                        continue;
                    }
                    current.Push(instruction.Operands[0]);
                }
                else if (instruction.Operator == Operator.And)
                {
                    var valuesFound = true;
                    if (!ushort.TryParse(instruction.Operands[0], out var value1) && !wires.TryGetValue(instruction.Operands[0], out value1))
                    {
                        current.Push(instruction.Operands[0]);
                        valuesFound = false;
                    }
                    if (!wires.TryGetValue(instruction.Operands[1], out var value2))
                    {
                        current.Push(instruction.Operands[1]);
                        valuesFound = false;
                    }
                    if (valuesFound)
                    {
                        wires.Add(current.Peek(), (ushort)(value1 & value2));
                    }
                }
                else if (instruction.Operator == Operator.Or)
                {
                    var valuesFound = true;
                    if (!wires.TryGetValue(instruction.Operands[0], out var value1))
                    {
                        current.Push(instruction.Operands[0]);
                        valuesFound = false;
                    }
                    if (!wires.TryGetValue(instruction.Operands[1], out var value2))
                    {
                        current.Push(instruction.Operands[1]);
                        valuesFound = false;
                    }
                    if (valuesFound)
                    {
                        wires.Add(current.Peek(), (ushort)(value1 | value2));
                    }
                }
                else if (instruction.Operator == Operator.LeftShift)
                {
                    if (wires.TryGetValue(instruction.Operands[0], out var value))
                    {
                        wires.Add(current.Peek(), (ushort)(value << int.Parse(instruction.Operands[1])));
                        continue;
                    }
                    current.Push(instruction.Operands[0]);
                }
                else if (instruction.Operator == Operator.RightShift)
                {
                    if (wires.TryGetValue(instruction.Operands[0], out var value))
                    {
                        wires.Add(current.Peek(), (ushort)(value >> int.Parse(instruction.Operands[1])));
                        continue;
                    }
                    current.Push(instruction.Operands[0]);
                }
            }
            return wires["a"];
        }

        private enum Operator { Assignment, Not, And, Or, LeftShift, RightShift }
        private record Instruction(Operator Operator, string[] Operands);
    }
}
