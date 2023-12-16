namespace AdventOfCode.Y2015.Solvers
{
    public class Day20 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var target = int.Parse(input);
            for (int house = 1; ; house++)
            {
                var presents = house.CalculateFactors().Sum() * 10;
                if (presents >= target)
                {
                    return house;
                }
            }
        }

        public override object SolvePart2(string input)
        {
            var target = int.Parse(input);
            for (int house = 1; ; house++)
            {
                var elves = house.CalculateFactors().ToArray();
                for (int i = 0; i < elves.Length; i++)
                {
                    if (elves[i] * 50 < house)
                    {
                        elves[i] = 0;
                    }
                }
                var presents = elves.Sum() * 11;
                if (presents >= target)
                {
                    return house;
                }
            }
        }
    }
}
