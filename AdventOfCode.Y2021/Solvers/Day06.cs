namespace AdventOfCode.Y2021.Solvers
{
    public class Day06 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            return GrowFish(input, 80).Values.Sum();
        }

        public override object SolvePart2(string input)
        {
            return GrowFish(input, 256).Values.Sum();
        }

        private static Dictionary<int, long> GrowFish(string input, int days)
        {
            var fish = input.Split(',').Select(int.Parse).ToList();
            var fishCounts = new Dictionary<int, long>()
            {
                { 0, fish.Count(x => x == 0) },
                { 1, fish.Count(x => x == 1) },
                { 2, fish.Count(x => x == 2) },
                { 3, fish.Count(x => x == 3) },
                { 4, fish.Count(x => x == 4) },
                { 5, fish.Count(x => x == 5) },
                { 6, fish.Count(x => x == 6) },
                { 7, fish.Count(x => x == 7) },
                { 8, fish.Count(x => x == 8) }
            };
            for (int day = 0; day < days; day++)
            {
                var tmp = 0L;
                for (int i = 0; i <= 8; i++)
                {
                    if (i == 0)
                    {
                        tmp = fishCounts[i];
                        continue;
                    }
                    fishCounts[i - 1] = fishCounts[i];
                }
                fishCounts[6] += tmp;
                fishCounts[8] = tmp;
            }
            return fishCounts;
        }
    }
}
