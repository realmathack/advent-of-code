namespace AdventOfCode.Y2021.Solvers
{
    public class Day08 : SolverWithLines
    {
        private readonly static int[] _lengths = [2, 3, 4, 7];
        public override object SolvePart1(string[] input)
        {
            var outputs = input.Select(line => line.Split(" | ")[1]).Select(output => output.Split(' ')).ToArray();
            return outputs.Sum(display => display.Count(segments => _lengths.Contains(segments.Length)));
        }

        private static readonly char[] _separator = [' ', '|'];
        public override object SolvePart2(string[] input)
        {
            var entries = input.Select(line => line.Split(_separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(segments => string.Concat(segments.Order())).ToArray()).ToArray();
            var sum = 0;
            foreach (var signals in entries)
            {
                SetSignals(signals, 7, "8");
                var one = new HashSet<char>(signals.First(signal => signal.Length == 2));
                SetSignals(signals, 2, "1");
                var four = new HashSet<char>(signals.First(signal => signal.Length == 4));
                SetSignals(signals, 4, "4");
                var seven = new HashSet<char>(signals.First(signal => signal.Length == 3));
                SetSignals(signals, 3, "7");
                var nine = signals.First(signal => signal.Length == 6 && one.IsSubsetOf(signal) && four.IsSubsetOf(signal) && seven.IsSubsetOf(signal));
                SetSignals(signals, nine, "9");
                var zero = signals.First(signal => signal.Length == 6 && one.IsSubsetOf(signal) && seven.IsSubsetOf(signal));
                SetSignals(signals, zero, "0");
                var six = signals.First(signal => signal.Length == 6);
                SetSignals(signals, six, "6");
                var three = signals.First(signal => signal.Length == 5 && one.IsSubsetOf(signal) && seven.IsSubsetOf(signal));
                SetSignals(signals, three, "3");
                // Set-wise difference between 2 & 5 -> 2 contains (4 - 1)
                four.ExceptWith(one);
                var five = signals.First(signal => signal.Length == 5 && four.IsSubsetOf(signal));
                SetSignals(signals, five, "5");
                var two = signals.First(signal => signal.Length == 5);
                SetSignals(signals, two, "2");
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

        private static void SetSignals(string[] signals, string signal, string newSignal)
        {
            var set = new HashSet<char>(signal);
            for (int i = 0; i < signals.Length; i++)
            {
                if (signals[i].Length > 1 && set.SetEquals(signals[i]))
                {
                    signals[i] = newSignal;
                }
            }
        }
    }
}
