namespace AdventOfCode.Y2023.Solvers
{
    public class Day09 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var history = input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
            return history.Sum(values => GetNextValue(values));
        }

        public override object SolvePart2(string[] input)
        {
            var history = input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
            return history.Sum(values => GetNextValue(values, true));
        }

        private static int GetNextValue(List<int> last, bool backwards = false)
        {
            var sequences = new List<List<int>>() { last };
            while (last.Any(value => value != 0))
            {
                var current = new List<int>();
                for (int i = 1; i < last.Count; i++)
                {
                    current.Add(last[i] - last[i - 1]);
                }
                sequences.Add(current);
                last = current;
            }
            if (backwards)
            {
                for (int i = sequences.Count - 2; i >= 0; i--)
                {
                    sequences[i].Insert(0, sequences[i][0] - sequences[i + 1][0]);
                }
                return sequences[0][0];
            }
            for (int i = sequences.Count - 2; i >= 0; i--)
            {
                sequences[i].Add(sequences[i][^1] + sequences[i + 1][^1]);
            }
            return sequences[0][^1];
        }
    }
}
