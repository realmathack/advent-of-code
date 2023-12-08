namespace AdventOfCode.Y2018.Solvers
{
    public class Day05 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            return ReduceReturnLength(input);
        }

        public override object SolvePart2(string input)
        {
            var shortest = input.Length;
            for (char c = 'a'; c <= 'z'; c++)
            {
                var improved = Improve(input, c);
                var length = ReduceReturnLength(improved);
                if (length < shortest)
                {
                    shortest = length;
                }
            }
            return shortest;
        }

        private static int ReduceReturnLength(string input)
        {
            var reduced = new StringBuilder(input);
            do
            {
                input = reduced.ToString();
                reduced.Clear();
                for (int i = 0; i < input.Length; i++)
                {
                    if (i + 1 != input.Length && input[i] != input[i + 1] && char.ToLower(input[i]) == char.ToLower(input[i + 1]))
                    {
                        i++;
                        continue;
                    }
                    reduced.Append(input[i]);
                }
            } while (reduced.Length != input.Length);
            return reduced.Length;
        }

        private static string Improve(string input, char c)
        {
            var improved = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (char.ToLower(input[i]) != c)
                {
                    improved.Append(input[i]);
                }
            }
            return improved.ToString();
        }
    }
}
