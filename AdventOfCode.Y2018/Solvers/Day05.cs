using System.Text;

namespace AdventOfCode.Y2018.Solvers
{
    public class Day05 : SolverWithText
    {
        public override object SolvePart1(string input) => ReduceReturnLength(input);

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

        private static int ReduceReturnLength(string polymer)
        {
            var reduced = new StringBuilder(polymer);
            do
            {
                polymer = reduced.ToString();
                reduced.Clear();
                for (int i = 0; i < polymer.Length; i++)
                {
                    if (i + 1 != polymer.Length && polymer[i] != polymer[i + 1] && char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1]))
                    {
                        i++;
                        continue;
                    }
                    reduced.Append(polymer[i]);
                }
            } while (reduced.Length != polymer.Length);
            return reduced.Length;
        }

        private static string Improve(string polymer, char c)
        {
            var improved = new StringBuilder(polymer.Length);
            for (int i = 0; i < polymer.Length; i++)
            {
                if (char.ToLower(polymer[i]) != c)
                {
                    improved.Append(polymer[i]);
                }
            }
            return improved.ToString();
        }
    }
}
