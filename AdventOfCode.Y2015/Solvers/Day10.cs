namespace AdventOfCode.Y2015.Solvers
{
    public class Day10 : SolverWithText
    {
        public override object SolvePart1(string input) => LookAndSay(input, 40);
        public override object SolvePart2(string input) => LookAndSay(input, 50);

        public static object LookAndSay(string input, int times)
        {
            for (int i = 0; i < times; i++)
            {
                input = LookAndSay(input);
            }
            return input.Length;
        }

        private static string LookAndSay(string input)
        {
            var sb = new StringBuilder(input.Length);
            var last = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[last] != input[i])
                {
                    sb.Append(CreateGroup(input[last], i - last));
                    last = i;
                }
            }
            sb.Append(CreateGroup(input[last], input.Length - last));
            return sb.ToString();
        }

        private static string CreateGroup(char digit, int length) => $"{length}{digit}";
    }
}
