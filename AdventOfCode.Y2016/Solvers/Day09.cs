namespace AdventOfCode.Y2016.Solvers
{
    public class Day09 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var decompressed = new StringBuilder();
            var current = 0;
            while (current < input.Length)
            {
                if (input[current] != '(')
                {
                    decompressed.Append(input[current++]);
                    continue;
                }
                var end = input.IndexOf(')', current++);
                var marker = input[current..end++].Split('x').Select(int.Parse).ToArray();
                var toRepeat = input[end..(end + marker[0])];
                for (int i = 0; i < marker[1]; i++)
                {
                    decompressed.Append(toRepeat);
                }
                current = end + marker[0];
            }
            return decompressed.Length;
        }

        public override object SolvePart2(string input)
        {
            var decompressedLength = 0L;
            var current = 0;
            while (current < input.Length)
            {
                if (input[current++] != '(')
                {
                    decompressedLength++;
                    continue;
                }
                var end = input.IndexOf(')', current);
                var marker = input[current..end++].Split('x').Select(int.Parse).ToArray();
                var toRepeat = input[end..(end + marker[0])];
                decompressedLength += marker[1] * (long)SolvePart2(toRepeat);
                current = end + marker[0];
            }
            return decompressedLength;
        }
    }
}
