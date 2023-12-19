namespace AdventOfCode.Y2020.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => TreesOnSlope(input, 3, 1);

        public override object SolvePart2(string[] input)
        {
            return (long)TreesOnSlope(input, 1, 1) * TreesOnSlope(input, 3, 1) * TreesOnSlope(input, 5, 1) * TreesOnSlope(input, 7, 1) * TreesOnSlope(input, 1, 2);
        }

        private static int TreesOnSlope(string[] lines, int right, int down)
        {
            var trees = 0;
            var width = lines[0].Length;
            var x = 0;
            for (int y = 0; y < lines.Length; y += down)
            {
                if (lines[y][x % width] == '#')
                {
                    trees++;
                }
                x += right;
            }
            return trees;
        }
    }
}
