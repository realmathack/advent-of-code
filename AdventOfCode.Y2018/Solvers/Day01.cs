namespace AdventOfCode.Y2018.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return input.Sum(int.Parse);
        }

        public override object SolvePart2(string[] input)
        {
            var changes = input.Select(int.Parse).ToList();
            var frequencies = new HashSet<int>();
            var i = 0;
            var current = 0;
            while (true)
            {
                current += changes[i];
                if (!frequencies.Add(current))
                {
                    return current;
                }
                i = ++i % changes.Count;
            }
        }
    }
}
