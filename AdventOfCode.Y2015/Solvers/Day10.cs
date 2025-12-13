namespace AdventOfCode.Y2015.Solvers
{
    public class Day10 : SolverWithText
    {
        public override object SolvePart1(string input) => LookAndSay(input, 40);
        public override object SolvePart2(string input) => LookAndSay(input, 50);

        public static int LookAndSay(string digits, int times)
        {
            for (int i = 0; i < times; i++)
            {
                digits = LookAndSay(digits);
            }
            return digits.Length;
        }

        private static string LookAndSay(string digits)
        {
            var sequence = new System.Text.StringBuilder(digits.Length);
            var last = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[last] != digits[i])
                {
                    sequence.Append(CreateGroup(digits[last], i - last));
                    last = i;
                }
            }
            sequence.Append(CreateGroup(digits[last], digits.Length - last));
            return sequence.ToString();
        }

        private static string CreateGroup(char digit, int length) => $"{length}{digit}";
    }
}
