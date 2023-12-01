namespace AdventOfCode.Y2021.Solvers
{
    public class Day08 : SolverWithLines
    {
        private readonly static int[] _lengths = [2, 3, 4, 7];
        public override object SolvePart1(string[] input)
        {
            var outputs = input.Select(x => x.Split(" | ")[1]).Select(x => x.Split(' ')).ToList();
            return outputs.Sum(x => x.Count(y => _lengths.Contains(y.Length)));
        }

        private static readonly char[] _separator = [' ', '|'];
        public override object SolvePart2(string[] input)
        {
            var entries = input.Select(x => x.Split(_separator, StringSplitOptions.RemoveEmptyEntries)).ToList();
            var sum = 0;
            foreach (var signals in entries)
            {
                SetSignals(signals, 7, "8");
                var one = signals.FirstOrDefault(x => x.Length == 2)?.ToCharArray() ?? [];
                SetSignals(signals, 2, "1");
                var four = signals.FirstOrDefault(x => x.Length == 4)?.ToCharArray() ?? [];
                SetSignals(signals, 4, "4");
                var seven = signals.FirstOrDefault(x => x.Length == 3)?.ToCharArray() ?? [];
                SetSignals(signals, 3, "7");

                do
                {
                    // 5 (2, 3, 5)  -> possible combos: 2 (), 3 (1, 7), 5 (4-1)
                    // 6 (0, 6, 9)  -> possible combos: 0 (1, 7), 6 (4-1), 9 (1, 4, 7)
                } while (!signals.All(x => x.Length == 1));
                sum += int.Parse($"{signals[10]}{signals[11]}{signals[12]}{signals[13]}");
            }
            return sum;
        }

        private static void SetSignals(string[] signals, int length, string newSignal)
        {
            for (int i = 0; i < signals.Length; i++)
            {
                if (signals[i].Length == length)
                {
                    signals[i] = newSignal;
                }
            }
        }
    }
}
