namespace AdventOfCode.Y2024.Solvers
{
    public class Day11 : SolverWithText
    {
        public override object SolvePart1(string input) => CalculateStoneCount(input, 25);
        public override object SolvePart2(string input) => CalculateStoneCount(input, 75);

        private static long CalculateStoneCount(string input, int blinks)
        {
            var stones = ToStones(input);
            var cache = new Dictionary<string, string[]>() { ["0"] = ["1"] };
            long count;
            for (int i = 0; i < blinks; i++)
            {
                var tmp = new Dictionary<string, long>(stones.Count);
                foreach (var stone in stones)
                {
                    if (cache.TryGetValue(stone.Key, out var cached))
                    {
                        foreach (var result in cached)
                        {
                            tmp[result] = tmp.TryGetValue(result, out count) ? count + stone.Value : stone.Value;
                        }
                        continue;
                    }
                    if (stone.Key.Length % 2 == 0)
                    {
                        var left = stone.Key[0..(stone.Key.Length / 2)].TrimStart('0');
                        if (left.Length == 0)
                        {
                            left = "0";
                        }
                        var right = stone.Key[(stone.Key.Length / 2)..].TrimStart('0');
                        if (right.Length == 0)
                        {
                            right = "0";
                        }
                        cache.Add(stone.Key, [left, right]);
                        tmp[left] = tmp.TryGetValue(left, out count) ? count + stone.Value : stone.Value;
                        tmp[right] = tmp.TryGetValue(right, out count) ? count + stone.Value : stone.Value;
                        continue;
                    }
                    var number = (long.Parse(stone.Key) * 2024L).ToString();
                    cache.Add(stone.Key, [number]);
                    tmp[number] = tmp.TryGetValue(number, out count) ? count + stone.Value : stone.Value;
                }
                stones = tmp;
            }
            return stones.Values.Sum();
        }

        private static Dictionary<string, long> ToStones(string input)
        {
            var stones = new Dictionary<string, long>();
            var numbers = input.Split(' ');
            foreach (var number in numbers)
            {
                stones[number] = stones.TryGetValue(number, out var count) ? count + 1 : 1;
            }
            return stones;
        }
    }
}
