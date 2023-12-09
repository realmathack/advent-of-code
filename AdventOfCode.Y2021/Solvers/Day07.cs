
namespace AdventOfCode.Y2021.Solvers
{
    public class Day07 : SolverWithText
    {
        public override object SolvePart1(string input) => FindLowestFuel(input, CalculateFuel1);

        public override object SolvePart2(string input) => FindLowestFuel(input, CalculateFuel2);

        private static int FindLowestFuel(string input, Func<List<int>, int, int> calculateFuel)
        {
            var positions = input.Split(',').Select(int.Parse).ToList();
            var lowest = int.MaxValue;
            for (int i = positions.Min(); i <= positions.Max(); i++)
            {
                var fuel = calculateFuel(positions, i);
                if (fuel < lowest)
                {
                    lowest = fuel;
                }
            }
            return lowest;
        }

        private static int CalculateFuel1(List<int> positions, int target) => positions.Sum(pos => Math.Abs(pos - target));

        private static int CalculateFuel2(List<int> positions, int target) => positions.Sum(pos => (Math.Abs(pos - target) * (Math.Abs(pos - target) + 1)) / 2);
    }
}
