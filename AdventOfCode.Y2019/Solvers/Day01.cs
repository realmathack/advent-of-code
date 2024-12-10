namespace AdventOfCode.Y2019.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => input.Select(int.Parse).Sum(CalculateFuel);

        public override object SolvePart2(string[] input)
        {
            var sum = 0;
            foreach (var mass in input.Select(int.Parse))
            {
                var fuel = 0;
                var tmp = mass;
                while (tmp != 0)
                {
                    tmp = CalculateFuel(tmp);
                    fuel += tmp;
                }
                sum += fuel;
            }
            return sum;
        }

        private static int CalculateFuel(int mass) => Math.Max(0, (mass / 3) - 2);
    }
}
