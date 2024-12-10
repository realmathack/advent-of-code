namespace AdventOfCode.Y2024.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var total = 0;
            foreach (var report in input)
            {
                var levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                if (IsSafe(levels))
                {
                    total++;
                }
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var total = 0;
            foreach (var report in input)
            {
                var levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int i = 0; i < levels.Length; i++)
                {
                    int[] tmp = [.. levels[0..i], .. levels[(i + 1)..]];
                    if (IsSafe(tmp))
                    {
                        total++;
                        break;
                    }
                }
            }
            return total;
        }

        private static bool IsSafe(int[] levels)
        {
            var direction = Direction.None;
            for (int i = 1; i < levels.Length; i++)
            {
                var difference = levels[i] - levels[i - 1];
                if (difference == 0 || Math.Abs(difference) > 3)
                {
                    return false;
                }
                if (difference < 0)
                {
                    if (direction == Direction.Increasing)
                    {
                        return false;
                    }
                    direction = Direction.Decreasing;
                }
                else
                {
                    if (direction == Direction.Decreasing)
                    {
                        return false;
                    }
                    direction = Direction.Increasing;
                }
            }
            return true;
        }

        private enum Direction { None, Increasing, Decreasing }
    }
}
