namespace AdventOfCode.Y2022.Solvers
{
    public class Day06 : SolverWithText
    {
        public override object SolvePart1(string input) => FindMarkerPosition(input, 4);
        public override object SolvePart2(string input) => FindMarkerPosition(input, 14);

        private static int FindMarkerPosition(string buffer, int charCount)
        {
            for (int i = 0; i < buffer.Length - charCount; i++)
            {
                var checkSet = new HashSet<char>(buffer.Substring(i, charCount));
                if (checkSet.Count == charCount)
                {
                    return i + charCount;
                }
            }
            return 0;
        }
    }
}
