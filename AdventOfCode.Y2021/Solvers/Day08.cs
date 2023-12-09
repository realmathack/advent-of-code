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
            var entries = input.Select(x => x.Split(_separator, StringSplitOptions.RemoveEmptyEntries).Select(y => new string(y.Order().ToArray())).ToArray()).ToList();
            var sum = 0;
            foreach (var signals in entries)
            {
                SetSignals(signals, 7, "8");
                var one = new HashSet<char>(signals.First(x => x.Length == 2));
                SetSignals(signals, 2, "1");
                var four = new HashSet<char>(signals.First(x => x.Length == 4));
                SetSignals(signals, 4, "4");
                var seven = new HashSet<char>(signals.First(x => x.Length == 3));
                SetSignals(signals, 3, "7");
                var nine = signals.First(x => x.Length == 6 && one.IsSubsetOf(x) && four.IsSubsetOf(x) && seven.IsSubsetOf(x));
                SetSignals(signals, nine, "9");
                var zero = signals.First(x => x.Length == 6 && one.IsSubsetOf(x) && seven.IsSubsetOf(x));
                SetSignals(signals, zero, "0");
                var six = signals.First(x => x.Length == 6);
                SetSignals(signals, six, "6");
                var three = signals.First(x => x.Length == 5 && one.IsSubsetOf(x) && seven.IsSubsetOf(x));
                SetSignals(signals, three, "3");
                // Set wise difference between 2 & 5 -> 2 contains (4 - 1)
                four.ExceptWith(one);
                var five = signals.First(x => x.Length == 5 && four.IsSubsetOf(x));
                SetSignals(signals, five, "5");
                var two = signals.First(x => x.Length == 5);
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
