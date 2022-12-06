using System.Data;

namespace AdventOfCode2022.Solvers
{
    public class Day06 : IBaseSolver
    {
        public string SolvePart1(string input)
        {
            var data = input.Trim();
            var result = GetMarkerPosition(data, 4);
            return result.ToString();
        }

        public string SolvePart2(string input)
        {
            var data = input.Trim();
            var result = GetMarkerPosition(data, 14);
            return result.ToString();
        }

        private static int GetMarkerPosition(string data, int charCount)
        {
            for (int i = 0; i < data.Length - charCount; i++)
            {
                var checkSet = new HashSet<char>(data.Substring(i, charCount));
                if (checkSet.Count == charCount)
                {
                    return i + charCount;
                }
            }
            return 0;
        }
    }
}
