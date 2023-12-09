namespace AdventOfCode.Y2015.Solvers
{
    public class Day01 : SolverWithText
    {
        public override object SolvePart1(string input) => input.Count(c => c == '(') - input.Count(c => c == ')');

        public override object SolvePart2(string input)
        {
            var floor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                floor += (input[i] == '(') ? 1 : -1;
                if (floor == -1)
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
