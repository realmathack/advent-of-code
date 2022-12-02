namespace AdventOfCode2022.Solvers
{
    public class Day02 : IBaseSolver
    {
        public string SolvePart1(string input)
        {
            var scores = new[]
            {
                4, 1, 7, // X: A, B, C
                8, 5, 2, // Y: A, B, C
                3, 9, 6  // Z: A, B, C
            };
            var total = ParseInput(input, scores);
            return total.ToString();
        }

        public string SolvePart2(string input)
        {
            var scores = new[]
            {
                3, 1, 2, // X: A, B, C
                4, 5, 6, // Y: A, B, C
                8, 9, 7  // Z: A, B, C
            };
            var total = ParseInput(input, scores );
            return total.ToString();
        }

        private static int ParseInput(string input, int[] scores)
        {
            var result = new List<int>();
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var score = ParseMatch(line, scores);
                result.Add(score);
            }
            return result.Sum();
        }

        private static int ParseMatch(string match, int[] scores)
        {
            var opponentShift = match[0] - 65;
            var resultShift = match[2] - 88;
            var index = (resultShift * 3 + opponentShift);
            return scores[index];
        }
    }
}
