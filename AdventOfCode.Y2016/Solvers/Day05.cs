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
                while (i < int.MaxValue)
                {
                    var hex = (input + i++).ToMD5Hex();
                    if (hex.StartsWith("00000"))
                    {
                        password += hex[5];
                        break;
                    }
                }
            }
            return password.ToLower();
        }

        public override object SolvePart2(string input)
        {
            var password = new char[8];
            var i = 0;
            for (int count = 0; count < 8; count++)
            {
                while (i < int.MaxValue)
                {
                    var hex = (input + i++).ToMD5Hex();
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
                }
            }
            return string.Concat(password).ToLower();
        }
    }
}
