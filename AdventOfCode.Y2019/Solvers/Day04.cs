namespace AdventOfCode.Y2019.Solvers
{
    public class Day04 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var limits = input.Split('-').Select(int.Parse).ToArray();
            return Enumerable.Range(limits[0], limits[1] - limits[0]).Count(IsPassword1);
        }

        public override object SolvePart2(string input)
        {
            var limits = input.Split('-').Select(int.Parse).ToArray();
            return Enumerable.Range(limits[0], limits[1] - limits[0]).Count(IsPassword2);
        }

        public static bool IsPassword1(int input)
        {
            var text = input.ToString();
            var hasAdjacentSame = false;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i - 1] > text[i])
                {
                    return false;
                }
                if (text[i - 1] == text[i])
                {
                    hasAdjacentSame = true;
                }
            }
            return hasAdjacentSame;
        }

        public static bool IsPassword2(int input)
        {
            var text = input.ToString();
            var adjacents = new Dictionary<char, HashSet<int>>();
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i - 1] > text[i])
                {
                    return false;
                }
                if (text[i - 1] == text[i])
                {
                    if (!adjacents.TryGetValue(text[i], out var positions))
                    {
                        adjacents[text[i]] = [];
                    }
                    adjacents[text[i]].Add(i - 1);
                    adjacents[text[i]].Add(i);
                }
            }
            return adjacents.Any(adjacent => adjacent.Value.Count == 2);
        }
    }
}
