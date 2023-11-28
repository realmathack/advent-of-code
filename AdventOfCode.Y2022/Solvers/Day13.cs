namespace AdventOfCode.Y2022.Solvers
{
    public class Day13 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var correctPairs = new List<int>();
            for (int section = 0; section < input.Length; section++)
            {
                var (left, right) = ToPair(input[section]);
                if (left.CompareTo(right) < 0)
                {
                    correctPairs.Add(section + 1);
                }
            }
            return correctPairs.Sum();
        }

        public override object SolvePart2(string[] input)
        {
            var lines = input.SelectMany(section => section.SplitIntoLines()).ToList();
            lines.Add("[[2]]");
            lines.Add("[[6]]");
            var signals = lines.Select(line => ParseSignals(line).list).ToList();
            signals.Sort();
            var decoderKey = FindDividerPacketIndex(signals, 2);
            decoderKey *= FindDividerPacketIndex(signals, 6, decoderKey);
            return decoderKey;
        }

        private static (SignalList left, SignalList right) ToPair(string section)
        {
            var lines = section.SplitIntoLines();
            var left = ParseSignals(lines[0]).list;
            var right = ParseSignals(lines[1]).list;
            return (left, right);
        }

        private static (SignalList list, int newPos) ParseSignals(string signals, int pos = 0)
        {
            var result = new SignalList();
            while (signals[++pos] != ']')
            {
                if (signals[pos] == ',')
                {
                    continue;
                }
                if (signals[pos] == '[')
                {
                    var (list, newPos) = ParseSignals(signals, pos);
                    result.Signals.Add(list);
                    pos = newPos;
                    continue;
                }
                var number = int.Parse(char.IsDigit(signals[pos + 1]) ? signals[pos..(++pos + 1)] : signals[pos].ToString());
                result.Signals.Add(new NumberSignal(number));
            }
            return (result, pos);
        }

        private static int FindDividerPacketIndex(List<SignalList> signals, int dividerPacket, int startPos = 0)
        {
            for (int i = startPos; i < signals.Count; i++)
            {
                if (signals[i].Signals.Count == 1 && signals[i].Signals[0] is SignalList list &&
                    list.Signals.Count == 1 && list.Signals[0] is NumberSignal numberSignal && numberSignal.Number == dividerPacket)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        private abstract class Signal : IComparable<Signal>
        {
            public abstract int CompareTo(Signal? other);
        }
        private class SignalList(params Signal[] signals) : Signal
        {
            public List<Signal> Signals { get; } = new List<Signal>(signals);

            public override int CompareTo(Signal? other)
            {
                if (other is null)
                {
                    return -1;
                }
                if (other is NumberSignal numberSignal)
                {
                    return CompareTo(new SignalList(numberSignal));
                }
                if (other is SignalList otherList)
                {
                    for (int i = 0; i < Signals.Count; i++)
                    {
                        if (i >= otherList.Signals.Count)
                        {
                            return 1;
                        }
                        var subCompare = Signals[i].CompareTo(otherList.Signals[i]);
                        if (subCompare == 0)
                        {
                            continue;
                        }
                        return subCompare;
                    }
                    if (Signals.Count < otherList.Signals.Count)
                    {
                        return -1;
                    }
                }
                return 0;
            }
        }
        private class NumberSignal(int number) : Signal
        {
            public int Number { get; } = number;

            public override int CompareTo(Signal? other)
            {
                if (other is null)
                {
                    return -1;
                }
                if (other is NumberSignal numberSignal)
                {
                    return Number - numberSignal.Number;
                }
                if (other is SignalList signalList)
                {
                    return new SignalList(this).CompareTo(signalList);
                }
                return -1;
            }
        }
    }
}
