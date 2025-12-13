namespace AdventOfCode.Y2022.Solvers
{
    public class Day20 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (list, zeroListItem) = ToCoordinates(input);
            Mix(list);
            return CalculateGroveCoordinates(zeroListItem, list.Length);
        }

        public override object SolvePart2(string[] input)
        {
            var (list, zeroListItem) = ToCoordinates(input, 811589153);
            Mix(list, 10);
            return CalculateGroveCoordinates(zeroListItem, list.Length);
        }

        private static void Mix(DoubleLinkedListItem[] list, int mixCount = 1)
        {
            var listItemCount = list.Length;
            for (int mixes = 0; mixes < mixCount; mixes++)
            {
                for (int i = 0; i < listItemCount; i++)
                {
                    var current = list[i];
                    var moveLeft = current.Value < 0L;
                    var value = Math.Abs(current.Value) % (listItemCount - 1L);
                    if (value == 0)
                    {
                        continue;
                    }
                    current.Remove();
                    var neighbor = current;
                    for (int moves = 0; moves < value; moves++)
                    {
                        neighbor = (moveLeft) ? neighbor.Previous : neighbor.Next;
                    }
                    if (moveLeft)
                    {
                        neighbor.InsertBefore(current);
                    }
                    else
                    {
                        neighbor.InsertAfter(current);
                    }
                }
            }
        }

        private static long CalculateGroveCoordinates(DoubleLinkedListItem zeroListItem, int listItemCount)
        {
            var sum = 0L;
            var offset = 1000 % listItemCount;
            for (int cycles = 0; cycles < 3; cycles++)
            {
                for (int i = 0; i < offset; i++)
                {
                    zeroListItem = zeroListItem.Next;
                }
                sum += zeroListItem.Value;
            }
            return sum;
        }

        private static (DoubleLinkedListItem[] List, DoubleLinkedListItem ZeroListItem) ToCoordinates(string[] lines, long multiplier = 1)
        {
            var list = new DoubleLinkedListItem[lines.Length];
            DoubleLinkedListItem? zeroListItem = null;
            list[0] = DoubleLinkedListItem.CreateFirst(long.Parse(lines[0]) * multiplier);
            for (int i = 1; i < lines.Length; i++)
            {
                list[i] = list[i - 1].CreateNext(long.Parse(lines[i]) * multiplier);
                if (list[i].Value == 0)
                {
                    zeroListItem = list[i];
                }
            }
            if (zeroListItem is null)
            {
                throw new ImpossibleException("ListItem with number 0 not found!");
            }
            return (list, zeroListItem);
        }

        private class DoubleLinkedListItem
        {
            private DoubleLinkedListItem _next;
            private DoubleLinkedListItem _previous;

            public long Value { get; init; }
            public DoubleLinkedListItem Next => _next;
            public DoubleLinkedListItem Previous => _previous;

            private DoubleLinkedListItem(long value, DoubleLinkedListItem previous, DoubleLinkedListItem next)
            {
                Value = value;
                _previous = previous;
                _next = next;
            }

            public void Remove()
            {
                _next._previous = _previous;
                _previous._next = _next;
            }

            public void InsertBefore(DoubleLinkedListItem newItem)
            {
                newItem._next = this;
                newItem._previous = _previous;
                _previous._next = newItem;
                _previous = newItem;
            }

            public void InsertAfter(DoubleLinkedListItem newItem)
            {
                newItem._previous = this;
                newItem._next = _next;
                _next._previous = newItem;
                _next = newItem;
            }

            public DoubleLinkedListItem CreateNext(long value)
            {
                var tmpNext = _next;
                _next = new(value, this, _next);
                tmpNext._previous = _next;
                return _next;
            }

            public static DoubleLinkedListItem CreateFirst(long value)
            {
                var first = new DoubleLinkedListItem(value, null!, null!);
                first._previous = first;
                first._next = first;
                return first;
            }

            public override string ToString()
            {
                return $"{{Value = {Value}}}";
            }
        }
    }
}
