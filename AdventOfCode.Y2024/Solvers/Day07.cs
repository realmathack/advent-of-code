namespace AdventOfCode.Y2024.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => Solve(input, ['+', '*']);
        public override object SolvePart2(string[] input) => Solve(input, ['+', '*', '|']);

        private static long Solve(string[] lines, char[] operators)
        {
            var total = 0L;
            foreach (var line in lines)
            {
                var (left, right) = line.SplitInTwo(": ");
                var testValue = long.Parse(left);
                var numbers = right.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                foreach (var permutation in operators.PermutationsWithRepetition(numbers.Length - 1))
                {
                    var result = numbers[0];
                    for (int i = 0; i < permutation.Length; i++)
                    {
                        result = permutation[i] switch
                        {
                            '+' => result += numbers[i + 1],
                            '*' => result *= numbers[i + 1],
                            '|' => result = long.Parse($"{result}{numbers[i + 1]}"),
                            _ => throw new InvalidOperationException($"Unknown operator: {permutation[i]}")
                        };
                    }
                    if (result == testValue)
                    {
                        total += testValue;
                        break;
                    }
                }
            }
            return total;
        }
    }
}
