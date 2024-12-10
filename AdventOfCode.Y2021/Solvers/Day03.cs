namespace AdventOfCode.Y2021.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var gamma = string.Empty;
            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = input.Count(line => line[i] == '1');
                gamma += (ones > (input.Length - ones)) ? '1' : '0';
            }
            var epsilon = string.Concat(gamma.Select(bit => bit == '1' ? '0' : '1'));
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        public override object SolvePart2(string[] input)
        {
            var oxygen = string.Empty;
            string[] lines = [.. input];
            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = lines.Count(line => line[i] == '1');
                var bit = (ones >= (lines.Length - ones)) ? '1' : '0';
                lines = lines.Where(line => line[i] == bit).ToArray();
                if (lines.Length == 1)
                {
                    oxygen = lines[0];
                }
            }
            var co2 = string.Empty;
            lines = [.. input];
            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = lines.Count(line => line[i] == '1');
                var bit = (ones >= (lines.Length - ones)) ? '0' : '1';
                lines = lines.Where(line => line[i] == bit).ToArray();
                if (lines.Length == 1)
                {
                    co2 = lines[0];
                }
            }
            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }
    }
}
