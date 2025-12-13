namespace AdventOfCode.Y2024.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => FindTotalCalibrationResult(input, ['+', '*']);
        public override object SolvePart2(string[] input) => FindTotalCalibrationResult(input, ['+', '*', '|']);

        private static long FindTotalCalibrationResult(string[] lines, char[] operators)
        {
            var total = 0L;
            foreach (var line in lines)
            {
                var (left, right) = line.SplitInTwo(": ");
                var target = long.Parse(left);
                var numbers = right.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                if (IsPossible(target, numbers, 1, numbers[0], operators))
                {
                    total += target;
                }
            }
            return total;
        }

        private static bool IsPossible(long target, long[] numbers, int index, long current, char[] operators)
        {
            if (index == numbers.Length)
            {
                return current == target;
            }
            var next = numbers[index++];
            foreach (var op in operators)
            {
                var result = op switch
                {
                    '+' => current + next,
                    '*' => current * next,
                    '|' => Concat(current, next),
                    _ => throw new ArgumentException($"Unknown operator: {op}")
                };
                if (result > target)
                {
                    continue;
                }
                if (IsPossible(target, numbers, index, result, operators))
                {
                    return true;
                }
            }
            return false;
        }

        private static long Concat(long left, long right)
        {
            var multiplier = 10L;
            while (right >= multiplier)
            {
                multiplier *= 10L;
            }
            return left * multiplier + right;
        }
    }
}
