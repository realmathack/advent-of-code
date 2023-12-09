namespace AdventOfCode.Y2020.Solvers
{
    public class Day09(int length) : SolverWithLines
    {
        public Day09() : this(25) { }

        public override object SolvePart1(string[] input) => FindInvalidNumber(input);

        public override object SolvePart2(string[] input)
        {
            var invalid = FindInvalidNumber(input);
            var numbers = input.Select(long.Parse).ToArray();
            for (int i = 0; i < numbers.Length; i++)
            {
                var sum = 0L;
                long[] range = [];
                var span = 2;
                while (sum < invalid)
                {
                    if (i + span >= numbers.Length)
                    {
                        break;
                    }
                    range = numbers[i..(i + span++)];
                    sum = range.Sum();
                }
                if (sum == invalid)
                {
                    return range.Min() + range.Max();
                }
            }
            return 0L;
        }

        private long FindInvalidNumber(string[] input)
        {
            var numbers = input.Select(long.Parse).ToArray();
            for (int i = length; i < numbers.Length; i++)
            {
                var preamble = numbers[(i - length)..i];
                var success = false;
                for (int j = 0; j < preamble.Length; j++)
                {
                    if (preamble.Any(number => preamble[j] != number && numbers[i] - preamble[j] == number))
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
