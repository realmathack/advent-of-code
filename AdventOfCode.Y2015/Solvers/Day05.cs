namespace AdventOfCode.Y2015.Solvers
{
    public class Day05 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return input.Count(IsNicePart1);
        }

        public override object SolvePart2(string[] input)
        {
            return input.Count(IsNicePart2);
        }

        private static bool IsNicePart1(string line)
        {
            if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
            {
                return false;
            }
            return HasDoubleLetter(line) && line.Count(IsVowel) >= 3;
        }

        private static bool IsVowel(char letter) => letter == 'a' || letter == 'e' || letter == 'i' || letter == 'o' || letter == 'u';

        private static bool HasDoubleLetter(string line)
        {
            for (int i = 1; i < line.Length; i++)
            {
                if (line[i - 1] == line[i])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsNicePart2(string line)
        {
            return HasDoubleLetterWithGap(line) && HasDoublePair(line);
        }

        private static bool HasDoubleLetterWithGap(string line)
        {
            for (int i = 2; i < line.Length; i++)
            {
                if (line[i - 2] == line[i])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasDoublePair(string line)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                var pair = line[i..(i + 2)];
                if (line[(i + 2)..].Contains(pair))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
