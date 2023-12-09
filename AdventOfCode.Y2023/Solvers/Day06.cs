namespace AdventOfCode.Y2023.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var records = input
                .Select(line => line[(line.IndexOf(':') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(parts => parts.Select(long.Parse).ToArray())
                .ToArray();
            var total = 1L;
            for (int i = 0; i < records[0].Length; i++)
            {
                total *= CountWaysToBeatRecord(records[0][i], records[1][i]);
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var record = input
                .Select(line => line[(line.IndexOf(':') + 1)..].Replace(" ", string.Empty))
                .Select(long.Parse)
                .ToArray();
            return CountWaysToBeatRecord(record[0], record[1]);
        }

        private static long CountWaysToBeatRecord(long time, long distance)
        {
            for (long i = 1L; i < time; i++)
            {
                if (i * (time - i) > distance)
                {
                    return time - 2 * i + 1;
                }
            }
            return 0L;
        }
    }
}
