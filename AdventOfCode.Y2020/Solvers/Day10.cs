namespace AdventOfCode.Y2020.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var adapters = input.Select(int.Parse).Order().ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.Last() + 3);
            var ones = adapters.Where((a, i) => i > 0 && (a - adapters[i - 1]) == 1).Count();
            var threes = adapters.Where((a, i) => i > 0 && (a - adapters[i - 1]) == 3).Count();
            return ones * threes;
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Implement (find a smart way to calculate distinct ways)
            return 0L;
        }
    }
}
