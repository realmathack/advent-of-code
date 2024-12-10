namespace AdventOfCode.Y2015.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToPresents(input).Select(ToAreas).Sum(areas => areas.Sum() * 2 + areas.Min());
        public override object SolvePart2(string[] input) => ToPresents(input).Sum(ToRibbon);

        private static int[][] ToPresents(string[] lines) => lines.Select(line => line.Split('x').Select(int.Parse).ToArray()).ToArray();
        private static int[] ToAreas(int[] present) => [present[0] * present[1], present[1] * present[2], present[2] * present[0]];
        private static int ToRibbon(int[] present) => (present.Sum() - present.Max()) * 2 + present.Product();
    }
}
