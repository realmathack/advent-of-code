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
            var signals = lines.Select(line => ToSignals(line).List).ToList();
            signals.Sort();
            var decoderKey = FindDividerPacketIndex(signals, 2);
            decoderKey *= FindDividerPacketIndex(signals, 6, decoderKey);
            return decoderKey;
        }

        private static (SignalList Left, SignalList Right) ToPair(string section)
        {
            var lines = section.SplitIntoLines();
            var left = ToSignals(lines[0]).List;
            var right = ToSignals(lines[1]).List;
            return (left, right);
        }

        private static (SignalList List, int NewPos) ToSignals(string signals, int pos = 0)
        {
            var list = new SignalList([]);
            while (signals[++pos] != ']')
            {
                if (signals[pos] == ',')
                {
                    continue;
                }
                if (signals[pos] == '[')
                {
                    var (tmp, newPos) = ToSignals(signals, pos);
                    list.Signals.Add(tmp);
                    pos = newPos;
                    continue;
                }
                var number = int.Parse(char.IsDigit(signals[pos + 1]) ? signals[pos..(++pos + 1)] : signals[pos].ToString());
                list.Signals.Add(new NumberSignal(number));
            }
            return (list, pos);
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

        private abstract record class Signal : IComparable<Signal>
        {
            public abstract int CompareTo(Signal? other);
        }

        private record class SignalList(List<Signal> Signals) : Signal
        {
            public override int CompareTo(Signal? other)
            {
                if (other is null)
                {
                    return -1;
                }
                if (other is NumberSignal numberSignal)
                {
                    return CompareTo(new SignalList([numberSignal]));
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

        private record class NumberSignal(int Number) : Signal
        {
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
                    return new SignalList([this]).CompareTo(signalList);
                }
                return -1;
            }
        }
    }
}
