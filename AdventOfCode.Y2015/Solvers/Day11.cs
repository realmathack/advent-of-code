namespace AdventOfCode.Y2015.Solvers
{
    public class Day11 : SolverWithText
    {
        public override object SolvePart1(string input) => FindNewPassword(input);
        public override object SolvePart2(string input) => FindNewPassword(FindNewPassword(input));

        private static string FindNewPassword(string password)
        {
            do
            {
                password = IncreasePassword(password);
            } while (!IsValidPassword(password));
            return password;
        }

        private static string IncreasePassword(string password, int offset = 1)
        {
            if (password[^offset] == 'z')
            {
                var tmp = password[..^offset] + 'a' + password[^(offset - 1)..];
                return IncreasePassword(tmp, ++offset);
            }
            return password[..^offset] + (char)(password[^offset] + 1) + password[^(offset - 1)..];
        }

        private static bool IsValidPassword(string password)
        {
            if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
            {
                return false;
            }
            var hasTriple = false;
            for (int i = 0; i < password.Length - 2; i++)
            {
                if (password[i] == password[i + 1] - 1 && password[i] == password[i + 2] - 2)
                {
                    hasTriple = true;
                    break;
                }
            }
            if (!hasTriple)
            {
                return false;
            }
            var pairs = new HashSet<char>();
            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i + 1])
                {
                    pairs.Add(password[i]);
                }
            }
            return pairs.Count >= 2;
        }
    }
}
