namespace AdventOfCode.Y2016.Solvers
{
    public class Day12 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ExecuteInstructions(input, new() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } });
        public override object SolvePart2(string[] input) => ExecuteInstructions(input, new() { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } });

        private static int ExecuteInstructions(string[] input, Dictionary<string, int> registers)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var parts = input[i].Split(' ');
                switch (parts[0])
                {
                    case "cpy":
                        registers[parts[2]] = int.TryParse(parts[1], out var tmp) ? tmp : registers[parts[1]];
                        break;
                    case "inc":
                        registers[parts[1]]++;
                        break;
                    case "dec":
                        registers[parts[1]]--;
                        break;
                    case "jnz":
                        var value = int.TryParse(parts[1], out tmp) ? tmp : registers[parts[1]];
                        if (value != 0)
                        {
                            i = i + int.Parse(parts[2]) - 1; // -1 to counteract i++;
                        }
                        break;
                }
            }
            return registers["a"];
        }
    }
}
