namespace AdventOfCode.Y2015.Solvers
{
    public class Day17 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => SolvePart1(input, 150);
        public static int SolvePart1(string[] input, int target)
        {
            var containers = ToContainers(input);
            return ToPossibilities(containers).Count(possibility => possibility.sum == target);
        }

        public override object SolvePart2(string[] input) => SolvePart2(input, 150);
        public static int SolvePart2(string[] input, int target)
        {
            var containers = ToContainers(input);
            var possibilitiesOnTarget = ToPossibilities(containers).Where(possibility => possibility.sum == target).ToList();
            var lowestCountContainers = possibilitiesOnTarget.Min(possibility => possibility.count);
            return possibilitiesOnTarget.Count(possibility => possibility.count == lowestCountContainers);
        }

        private static List<int> ToContainers(string[] lines) => lines.Select(int.Parse).ToList();
        private static List<(int sum, int count)> ToPossibilities(List<int> containers) => containers.PowerSet().Select(set => (set.Sum(), set.Count())).ToList();
    }
}
