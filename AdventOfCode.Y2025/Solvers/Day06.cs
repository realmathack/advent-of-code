namespace AdventOfCode.Y2025.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var numbers = input[..^1].Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray()).ToArray();
            var operators = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var total = 0L;
            for (int i = 0; i < operators.Length; i++)
            {
                var tmp = numbers.Select(line => line[i]);
                total += (operators[i] == "+") ? tmp.Sum() : tmp.Product();
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var total = 0L;
            var numbers = new List<long>();
            var op = input[^1][0];
            for (int i = 0; i < input[^1].Length; i++)
            {
                var number = string.Concat(input[..^1].Select(line => line[i]).Where(c => c != ' '));
                if (number != string.Empty)
                {
                    numbers.Add(long.Parse(number));
                }
                if (number == string.Empty || i == (input[^1].Length - 1))
                {
                    total += (op == '+') ? numbers.Sum() : numbers.Product();
                }
                if (number == string.Empty)
                {
                    numbers.Clear();
                    op = input[^1][i + 1];
                }
            }
            return total;
        }
    }
}
