namespace AdventOfCode2022.Solvers
{
    public class Day01 : IBaseSolver
    {
        public string SolvePart1(string input)
        {
            var values = ParseInput(input);
            var highest = values.Max();
            return highest.ToString();
        }

        public string SolvePart2(string input)
        {
            var values = ParseInput(input);
            var top3 = values.OrderByDescending(x => x).Take(3).Sum();
            return top3.ToString();
        }

        private static List<int> ParseInput(string input)
        {
            var result = new List<int>();
            var blocks = input.Split(Environment.NewLine + Environment.NewLine);
            foreach (var block in blocks)
            {
                var lines = block.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var total = lines.Sum(line => int.Parse(line));
                result.Add(total);
            }
            return result;
        }
    }
}
