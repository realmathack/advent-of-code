namespace AdventOfCode.Y2020.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var expenses = input.Select(int.Parse).ToArray();
            for (int i = 0; i < expenses.Length; i++)
            {
                for (int j = i; j < expenses.Length; j++)
                {
                    if (expenses[i] + expenses[j] == 2020)
                    {
                        return expenses[i] * expenses[j];
                    }
                }
            }
            return 0;
        }

        public override object SolvePart2(string[] input)
        {
            var expenses = input.Select(int.Parse).ToArray();
            for (int i = 0; i < expenses.Length; i++)
            {
                for (int j = i; j < expenses.Length; j++)
                {
                    for (int k = j; k < expenses.Length; k++)
                    {
                        if (expenses[i] + expenses[j] + expenses[k] == 2020)
                        {
                            return (long)expenses[i] * expenses[j] * expenses[k];
                        }
                    }
                }
            }
            return 0L;
        }
    }
}
