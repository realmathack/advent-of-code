namespace AdventOfCode.Y2018.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var two = 0;
            var three = 0;
            foreach (var id in input)
            {
                var counts = id.GroupBy(c => c).Select(g => g.Count()).Distinct().ToArray();
                if (counts.Contains(2))
                {
                    two++;
                }
                if (counts.Contains(3))
                {
                    three++;
                }
            }
            return two * three;
        }

        public override object SolvePart2(string[] input)
        {
            for (int a = 0; a < input.Length; a++)
            {
                for (int b = a + 1; b < input.Length; b++)
                {
                    var differences = new List<int>();
                    for (int i = 0; i < input[a].Length; i++)
                    {
                        if (input[a][i] != input[b][i])
                        {
                            differences.Add(i);
                        }
                    }
                    if (differences.Count == 1)
                    {
                        return input[a].Remove(differences[0], 1);
                    }
                }
            }
            return null!;
        }
    }
}
