namespace AdventOfCode.Y2015.Solvers
{
    public class Day17(int _target) : SolverWithLines
    {
        public Day17() : this(150) { }

        public override object SolvePart1(string[] input) => ToPossibilities(ToContainers(input)).Count(possibility => possibility.Sum == _target);

        public override object SolvePart2(string[] input)
        {
            var possibilitiesOnTarget = ToPossibilities(ToContainers(input)).Where(possibility => possibility.Sum == _target).ToArray();
            var lowestCountContainers = possibilitiesOnTarget.Min(possibility => possibility.Count);
            return possibilitiesOnTarget.Count(possibility => possibility.Count == lowestCountContainers);
        }

        private static int[] ToContainers(string[] lines) => lines.Select(int.Parse).ToArray();
        private static (int Sum, int Count)[] ToPossibilities(int[] containers) => containers.PowerSet().Select(set => (set.Sum(), set.Count)).ToArray();
    }
}
