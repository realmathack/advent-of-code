using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public class Day08 : SolverWithLines
    {
        private readonly Regex _hexCharRegex = new(@"\\x([0-9a-f]{2})");

        public override object SolvePart1(string[] input)
        {
            return input.Sum(line => line.Length) - input.Sum(ToMemoryLength);
        }

        public override object SolvePart2(string[] input)
        {
            return input.Sum(ToEncodeLength) - input.Sum(line => line.Length);
        }

        private int ToMemoryLength(string line)
        {
            line = line.Replace(@"\\", "?").Replace(@"\""", "?");
            line = _hexCharRegex.Replace(line, "?");
            return line.Length - 2;
        }

        private static int ToEncodeLength(string line)
        {
            line = line.Replace(@"\", "??").Replace(@"""", "??");
            return line.Length + 2;
        }
    }
}
