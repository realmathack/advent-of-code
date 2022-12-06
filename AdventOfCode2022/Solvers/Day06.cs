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
            var result = 0;
            var checkSet = new HashSet<char>(charCount);
            for (int i = 0; i < data.Length - charCount; i++)
            {
                for (int j = 0; j < charCount; j++)
                {
                    if (checkSet.Contains(data[i+j]))
                    {
                        break;
                    }
                    else
                    {
                        checkSet.Add(data[i+j]);
                    }
                }
                if (checkSet.Count == charCount)
                {
                    result = i + charCount;
                    break;
                }
                checkSet.Clear();
            }
            return result;
        }
    }
}
