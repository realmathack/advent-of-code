namespace AdventOfCode.Y2017.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => input.Where(line => line.Split(' ').GroupBy(pass => pass).All(g => g.Count() == 1)).Count();

        public override object SolvePart2(string[] input)
        {
            return input.Where(line => line.Split(' ').Select(word => string.Concat(word.Order())).ToArray().GroupBy(word => word).All(g => g.Count() == 1)).Count();
        }
    }
}
