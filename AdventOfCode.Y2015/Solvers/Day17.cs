namespace AdventOfCode.Y2015.Solvers
{
    public class Day17(int target) : SolverWithLines
    {
        public Day17() : this(150) { }

        public override object SolvePart1(string[] input) => ToPossibilities(ToContainers(input)).Count(possibility => possibility.Sum == target);

        public override object SolvePart2(string[] input)
        {
            var containers = ToContainers(input);
            var possibilitiesOnTarget = ToPossibilities(containers).Where(possibility => possibility.Sum == target).ToList();
            var lowestCountContainers = possibilitiesOnTarget.Min(possibility => possibility.Count);
            return possibilitiesOnTarget.Count(possibility => possibility.Count == lowestCountContainers);
        }

        private static List<int> ToContainers(string[] lines) => lines.Select(int.Parse).ToList();
        private static List<(int Sum, int Count)> ToPossibilities(List<int> containers) => containers.PowerSet().Select(set => (set.Sum(), set.Count)).ToList();
    }
}
