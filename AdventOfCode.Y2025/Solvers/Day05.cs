using Range = AdventOfCode.Range<long>;

namespace AdventOfCode.Y2025.Solvers
{
    public class Day05 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var (freshRanges, ingredients) = ToFreshRangesAndIngredients(input);
            var total = 0;
            foreach (var ingredient in ingredients)
            {
                foreach (var range in freshRanges)
                {
                    if (range.Start <= ingredient && ingredient <= range.End)
                    {
                        total++;
                        break;
                    }
                    if (range.Start > ingredient)
                    {
                        break;
                    }
                }
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var (freshRanges, _) = ToFreshRangesAndIngredients(input);
            var total = 0L;
            var last = 0L;
            foreach (var range in freshRanges)
            {
                if (range.End <= last)
                {
                    continue;
                }
                total += 1L + range.End - Math.Max(range.Start, last + 1L);
                last = range.End;
            }
            return total;
        }

        private static (Range[] FreshRanges, long[] Ingredients) ToFreshRangesAndIngredients(string[] lineGroups)
        {
            var freshRanges = lineGroups[0].SplitIntoLines()
                .Select(Range.Parse)
                .OrderBy(range => range.Start)
                .ToArray();
            var ingredients = lineGroups[1].SplitIntoLines()
                .Select(long.Parse)
                .ToArray();
            return (freshRanges, ingredients);
        }
    }
}
