namespace AdventOfCode.Y2015.Solvers
{
    public class Day20 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var target = int.Parse(input);
            for (int house = 1; ; house++)
            {
                var presents = GetElves(house).Sum() * 10;
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
                var elves = GetElves(house).ToArray();
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

        private static HashSet<int> GetElves(int house)
        {
            // https://rosettacode.org/wiki/Factors_of_an_integer#C++
            var result = new HashSet<int>() { 1, house };
            for (int i = 2; i * i < house; i++)
            {
                if (house % i == 0)
                {
                    result.Add(i);
                    if (i * i != house)
                    {
                        result.Add(house / i);
                    }
                }
            }
            return result;
        }
    }
}
