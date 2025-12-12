using System.Text.RegularExpressions;

namespace AdventOfCode.Y2025.Solvers
{
    public partial class Day12 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var presentSizes = input[..^1].Select(present => present.Count(c => c == '#')).ToArray();
            var regions = ToRegions(input[^1].SplitIntoLines());
            return regions.Count(region => DoPresentsFit(region, presentSizes));
        }

        public override object SolvePart2(string[] input) => "Last Day";

        private static Region[] ToRegions(string[] lines)
        {
            var regions = new Region[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var match = RegionRegex().Match(lines[i]);
                regions[i] = new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), [.. match.Groups[3].Value.Split(' ').Select(int.Parse)]);
            }
            return regions;
        }

        private static bool DoPresentsFit(Region region, int[] presentSizes)
        {
            var totalPresentSizes = 0;
            for (int i = 0; i < region.PresentCounts.Length; i++)
            {
                totalPresentSizes += region.PresentCounts[i] * presentSizes[i];
            }
            if (region.Width * region.Height < totalPresentSizes)
            {
                return false; // Cannot fit
            }
            if ((region.Width / 3) * (region.Height / 3) >= region.PresentCounts.Sum())
            {
                return true; // Fit for sure (all shapes are 3x3)
            }
            return false; // Maybe
        }

        [GeneratedRegex(@"(\d+)x(\d+): (.+)")]
        private static partial Regex RegionRegex();

        private record class Region(int Width, int Height, int[] PresentCounts);
    }
}
