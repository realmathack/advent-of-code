namespace AdventOfCode.Y2017.Solvers
{
    public class Day01 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var sum = input[0] == input[^1] ? input[0] - '0' : 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i + 1])
                {
                    sum += input[i] - '0';
                }
            }
            return sum;
        }

        public override object SolvePart2(string input)
        {
            var sum = 0;
            var offset = input.Length / 2;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + offset) % input.Length])
                {
                    sum += input[i] - '0';
                }
            }
            return sum;
        }
    }
}
