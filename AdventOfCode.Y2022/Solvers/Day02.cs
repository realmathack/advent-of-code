namespace AdventOfCode.Y2022.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var scoreList = new[]
            {
                3+1, 6+2, 0+3, // A: XYZ
                0+1, 3+2, 6+3, // B: XYZ
                6+1, 0+2, 3+3  // C: XYZ
            };
            return input.Select(line => scoreList[(line[0] - 'A') * 3 + (line[2] - 'X')]).Sum();
        }

        public override object SolvePart2(string[] input)
        {
            var scoreList = new[]
            {
                0+3, 3+1, 6+2, // A: XYZ
                0+1, 3+2, 6+3, // B: XYZ
                0+2, 3+3, 6+1  // C: XYZ
            };
            return input.Select(line => scoreList[(line[0] - 'A') * 3 + (line[2] - 'X')]).Sum();
        }
    }
}
