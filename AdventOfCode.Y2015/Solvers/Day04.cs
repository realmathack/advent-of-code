using System.Security.Cryptography;

namespace AdventOfCode.Y2015.Solvers
{
    public class Day04 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            return FindFirstHashStartingWith(input, "00000");
        }

        public override object SolvePart2(string input)
        {
            return FindFirstHashStartingWith(input, "000000");
        }

        private static object FindFirstHashStartingWith(string input, string start)
        {
            int i = 0;
            do
            {
                i++;
                var bytes = Encoding.ASCII.GetBytes(input + i);
                var hash = MD5.HashData(bytes);
                var hex = Convert.ToHexString(hash);
                if (hex.StartsWith(start))
                {
                    return i;
                }
            } while (i < int.MaxValue);
            return 0;
        }
    }
}
