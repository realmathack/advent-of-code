namespace AdventOfCode.Y2020.Solvers
{
    public class Day09 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => SolvePart1(input, 25);
        public static long SolvePart1(string[] input, int length)
        {
            var numbers = input.Select(long.Parse).ToArray();
            for (int i = length; i < numbers.Length; i++)
            {
                var previous = numbers[(i - length)..i];
                var success = false;
                foreach (var first in previous)
                {
                    if (previous.Any(x => x != first && x == numbers[i] - first))
                    {
                        success = true;
                    }
                }
                if (!success)
                {
                    return numbers[i];
                }
            }
            return 0L;
        }

        public override object SolvePart2(string[] input) => SolvePart2(input, 25);
        public static long SolvePart2(string[] input, int length)
        {
            var invalid = SolvePart1(input, length);
            var numbers = input.Select(long.Parse).ToArray();
            for (int i = 0; i < numbers.Length; i++)
            {
                var sum = 0L;
                long[] range = [];
                var span = 2;
                do
                {
                    if (i + span >= numbers.Length)
                    {
                        break;
                    }
                    range = numbers[i..(i + span++)];
                    sum = range.Sum();
                } while (sum < invalid);
                if (sum == invalid)
                {
                    return range.Min() + range.Max();
                }
            }
            return 0L;
        }
    }
}
