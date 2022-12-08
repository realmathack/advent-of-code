namespace AdventOfCode.Y2015.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var paper = 0;
            foreach (var present in ToPresents(input))
            {
                var areas = ToAreas(present);
                paper += areas.Sum() * 2 + areas.Min();
            }
            return paper;
        }

        public override object SolvePart2(string[] input)
        {
            return ToPresents(input).Select(ToRibbon).Sum();
        }

        private static List<int[]> ToPresents(string[] lines)
        {
            var presents = new List<int[]>();
            foreach (var line in lines)
            {
                var parts = line.Split('x');
                presents.Add(parts.Select(int.Parse).ToArray());
            }
            return presents;
        }

        private static List<int> ToAreas(int[] present) => new() { present[0] * present[1], present[1] * present[2], present[2] * present[0] };
        private static int ToRibbon(int[] present) => (present.Sum() - present.Max()) * 2 + present[0] * present[1] * present[2];
    }
}
