using System.Security.Cryptography;

namespace AdventOfCode.Y2016.Solvers
{
    public class Day05 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var password = string.Empty;
            var i = 0;
            for (int count = 0; count < 8; count++)
            {
                do
                {
                    var bytes = Encoding.ASCII.GetBytes(input + i++);
                    var hash = MD5.HashData(bytes);
                    var hex = Convert.ToHexString(hash);
                    if (hex.StartsWith("00000"))
                    {
                        password += hex[5];
                        break;
                    }
                } while (i < int.MaxValue);
            }
            return password.ToLower();
        }

        public override object SolvePart2(string input)
        {
            var password = new char[8];
            var i = 0;
            for (int count = 0; count < 8; count++)
            {
                do
                {
                    var bytes = Encoding.ASCII.GetBytes(input + i++);
                    var hash = MD5.HashData(bytes);
                    var hex = Convert.ToHexString(hash);
                    if (hex.StartsWith("00000"))
                    {
                        int pos;
                        if ((pos = hex[5] - '0') >= 8 || password[pos] != default)
                        {
                            continue;
                        }
                        password[pos] = hex[6];
                        break;
                    }
                } while (i < int.MaxValue);
            }
            return string.Join("", password).ToLower();
        }
    }
}
