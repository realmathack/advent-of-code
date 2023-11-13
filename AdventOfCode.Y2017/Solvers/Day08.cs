namespace AdventOfCode.Y2017.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var registers = new Dictionary<string, int>();
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                var testValue = registers.TryGetValue(parts[4], out var tmp) ? tmp : 0;
                var conditionValue = int.Parse(parts[6]);
                var success = parts[5] switch
                {
                    "<" => testValue < conditionValue,
                    "<=" => testValue <= conditionValue,
                    "==" => testValue == conditionValue,
                    "!=" => testValue != conditionValue,
                    ">=" => testValue >= conditionValue,
                    ">" => testValue > conditionValue,
                    _ => false
                };
                if (success)
                {
                    var value = registers.TryGetValue(parts[0], out tmp) ? tmp : 0;
                    var delta = int.Parse(parts[2]);
                    value = (parts[1] == "inc") ? value + delta : value - delta;
                    registers[parts[0]] = value;
                }
            }
            return registers.Max(x => x.Value);
        }

        public override object SolvePart2(string[] input)
        {
            var registers = new Dictionary<string, int>();
            var highestValue = 0;
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                var testValue = registers.TryGetValue(parts[4], out var tmp) ? tmp : 0;
                var conditionValue = int.Parse(parts[6]);
                var success = parts[5] switch
                {
                    "<" => testValue < conditionValue,
                    "<=" => testValue <= conditionValue,
                    "==" => testValue == conditionValue,
                    "!=" => testValue != conditionValue,
                    ">=" => testValue >= conditionValue,
                    ">" => testValue > conditionValue,
                    _ => false
                };
                if (success)
                {
                    var value = registers.TryGetValue(parts[0], out tmp) ? tmp : 0;
                    var delta = int.Parse(parts[2]);
                    value = (parts[1] == "inc") ? value + delta : value - delta;
                    registers[parts[0]] = value;
                    if (value > highestValue)
                    {
                        highestValue = value;
                    }
                }
            }
            return highestValue;
        }
    }
}
