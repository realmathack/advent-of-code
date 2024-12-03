namespace AdventOfCode.Y2021.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => input.Sum(CalculateErrorScore);

        public override object SolvePart2(string[] input)
        {
            var scores = input.Select(CalculateAutoCompleteScore).Where(score => score > 0).Order().ToArray();
            return scores[scores.Length / 2];
        }

        private static readonly Dictionary<char, int> _errorScores = new() { [')'] = 3, [']'] = 57, ['}'] = 1197, ['>'] = 25137 };
        private int CalculateErrorScore(string line)
        {
            var stack = new Stack<char>();
            foreach (var character in line)
            {
                if (!_errorScores.ContainsKey(character))
                {
                    stack.Push(character);
                    continue;
                }
                if (Math.Abs(stack.Pop() - character) > 2)
                {
                    return _errorScores[character];
                }
            }
            return 0;
        }

        private static readonly Dictionary<char, long> _autoCompleteScores = new() { ['('] = 1L, ['['] = 2L, ['{'] = 3L, ['<'] = 4L };
        private long CalculateAutoCompleteScore(string line)
        {
            var stack = new Stack<char>();
            foreach (var character in line)
            {
                if (_autoCompleteScores.ContainsKey(character))
                {
                    stack.Push(character);
                    continue;
                }
                if (Math.Abs(stack.Pop() - character) > 2)
                {
                    return 0;
                }
            }
            var sum = 0L;
            while (stack.TryPop(out var character))
            {
                sum = sum * 5L + _autoCompleteScores[character];
            }
            return sum;
        }
    }
}
