namespace AdventOfCode.Y2018.Solvers
{
    public class Day12 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input) => CalculateGenerationSums(input[0][15..], ToNotes(input[1]), 20)[^1];

        public override object SolvePart2(string[] input)
        {
            var sums = CalculateGenerationSums(input[0][15..], ToNotes(input[1]), 500);
            return sums[^1] + (50000000000 - 500) * (sums[^1] - sums[^2]);
        }

        private static List<int> CalculateGenerationSums(string pots, Dictionary<string, char> notes, int generations)
        {
            var sums = new List<int>();
            var firstPot = 0;
            for (int generation = 0; generation < generations; generation++)
            {
                while (!pots.StartsWith("...."))
                {
                    pots = "." + pots;
                    firstPot--;
                }
                while (!pots.EndsWith("...."))
                {
                    pots += ".";
                }
                var nextGeneration = "..";
                for (int i = 2; i < pots.Length - 2; i++)
                {
                    var pattern = pots[(i - 2)..(i + 3)];
                    nextGeneration += notes.TryGetValue(pattern, out var plant) ? plant : '.';
                }
                pots = nextGeneration + "..";
                sums.Add(pots.Select((plant, i) => plant == '#' ? firstPot + i : 0).Sum());
            }
            return sums;
        }

        private static Dictionary<string, char> ToNotes(string notes)
        {
            return notes.SplitIntoLines().Select(line => line.SplitInTwo(" => ")).ToDictionary(parts => parts.Left, parts => parts.Right[0]);
        }
    }
}
