namespace AdventOfCode.Y2020.Solvers
{
    public class Day09(int length) : SolverWithLines
    {
        public Day09() : this(25) { }

        public override object SolvePart1(string[] input) => GetInvalidNumber(input);

        public override object SolvePart2(string[] input)
        {
            var invalid = GetInvalidNumber(input);
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

        private long GetInvalidNumber(string[] input)
        {
            var numbers = input.Select(long.Parse).ToArray();
            for (int i = length; i < numbers.Length; i++)
            {
                var previous = numbers[(i - length)..i];
                var success = false;
                foreach (var first in previous)
                {
                    if (previous.Any(number => number != first && number == numbers[i] - first))
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
    }
}
