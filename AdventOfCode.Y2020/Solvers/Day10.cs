namespace AdventOfCode.Y2020.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var adapters = input.Select(int.Parse).Order().ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.Last() + 3);
            var ones = adapters.Where((adapter, index) => index > 0 && (adapter - adapters[index - 1]) == 1).Count();
            var threes = adapters.Where((adapter, index) => index > 0 && (adapter - adapters[index - 1]) == 3).Count();
            return ones * threes;
        }

        public override object SolvePart2(string[] input)
        {
            var sum = 1L;
            var adapters = input.Select(int.Parse).Order().ToList();
            var current = 0;
            adapters.Insert(0, 0);
            var end = adapters.Last() + 3;
            adapters.Add(end);
            var currentBlock = new HashSet<int>();
            while (current != end)
            {
                var block = adapters.Where(adapter => adapter > current && adapter <= current + 3).ToArray();
                if (block.Length == 1)
                {
                    if (currentBlock.Count > 1)
                    {
                        var rangeDecrease = (currentBlock.Max() - currentBlock.Min()) / 3;
                        sum *= (long)Math.Pow(2, currentBlock.Count - 1) - rangeDecrease;
                    }
                    currentBlock.Clear();
                }
                else
                {
                    currentBlock.UnionWith(block);
                }
                current = block.Min();
            }
            return sum;
        }
    }
}
