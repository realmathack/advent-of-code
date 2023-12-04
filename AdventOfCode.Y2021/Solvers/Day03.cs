namespace AdventOfCode.Y2021.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var gamma = string.Empty;
            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = input.Count(x => x[i] == '1');
                gamma += (ones > (input.Length - ones)) ? '1' : '0';
            }
            var epsilon = new string(gamma.Select(x => x == '1' ? '0' : '1').ToArray());
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        public override object SolvePart2(string[] input)
        {
            var oxygen = string.Empty;
            var list = new List<string>(input);
            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = list.Count(x => x[i] == '1');
                var bit = (ones >= (list.Count - ones)) ? '1' : '0';
                list = list.Where(x => x[i] == bit).ToList();
                if (list.Count == 1)
                {
                    oxygen = list[0];
                }
            }
            var co2 = string.Empty;
            list = new List<string>(input);
            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = list.Count(x => x[i] == '1');
                var bit = (ones >= (list.Count - ones)) ? '0' : '1';
                list = list.Where(x => x[i] == bit).ToList();
                if (list.Count == 1)
                {
                    co2 = list[0];
                }
            }
            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }
    }
}