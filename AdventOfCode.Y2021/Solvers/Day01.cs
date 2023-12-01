namespace AdventOfCode.Y2021.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var count = 0;
            var depths = input.Select(int.Parse).ToArray();
            for (int i = 1; i < depths.Length; i++)
            {
                if (depths[i - 1] < depths[i])
                {
                    count++;
                }
            }
            return count;
        }

        public override object SolvePart2(string[] input)
        {
            var count = 0;
            var depths = input.Select(int.Parse).ToArray();
            for (int i = 3; i < depths.Length; i++)
            {
                if (depths[(i - 3)..i].Sum() < depths[(i - 2)..(i + 1)].Sum())
                {
                    count++;
                }
            }
            return count;
        }
    }
}
