using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => input.Sum(line => line.Length) - input.Sum(ToMemoryLength);
        public override object SolvePart2(string[] input) => input.Sum(ToEncodeLength) - input.Sum(line => line.Length);

        private static readonly Regex _hexCharRegex = new(@"\\x([0-9a-f]{2})");
        private static int ToMemoryLength(string line)
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
