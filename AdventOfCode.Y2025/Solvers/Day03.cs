namespace AdventOfCode.Y2025.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => input.Sum(line => GetMaxJoltage(line, 2));
        public override object SolvePart2(string[] input) => input.Sum(line => GetMaxJoltage(line, 12));

        private static long GetMaxJoltage(ReadOnlySpan<char> bank, int digitCount)
        {
            var max = new char[digitCount];
            var currentDigit = 0;
            var nextStartPos = 0;
            while (currentDigit < digitCount)
            {
                var currentMax = '0';
                var currentMaxPos = 0;
                for (int i = nextStartPos; i <= bank.Length - (digitCount - currentDigit); i++)
                {
                    if (bank[i] > currentMax)
                    {
                        currentMax = bank[i];
                        currentMaxPos = i;
                    }
                }
                max[currentDigit++] = currentMax;
                nextStartPos = currentMaxPos + 1;
            }
            return long.Parse(max.AsSpan());
        }
    }
}
