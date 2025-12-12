using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016.Solvers
{
    public partial class Day11 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => new BFS().Search(ToInitialState(input)).Count;
        public override object SolvePart2(string[] input) => new BFS().Search(ToInitialState(input, true)).Count;

        private static List<State> GetNextStates(State current)
        {
            var nextStates = new List<State>();
            var potentialFloors = new[] { current.CurrentFloor - 1, current.CurrentFloor + 1 }.Where(floor => floor >= 0 && floor < 4).ToArray();
            foreach (var potentialFloor in potentialFloors)
            {
                foreach (var chip1 in current.CurrentChips())
                {
                    nextStates.Add(current.MoveItemToFloor(chip1, potentialFloor));
                    foreach (var chip2 in current.CurrentChips().Where(chip => chip != chip1))
                    {
                        nextStates.Add(current.MoveItemToFloor(chip1|chip2, potentialFloor));
                    }
                }
                foreach (var generator1 in current.CurrentGenerators())
                {
                    nextStates.Add(current.MoveItemToFloor(generator1, potentialFloor));
                    foreach (var generator2 in current.CurrentGenerators().Where(generator => generator != generator1))
                    {
                        nextStates.Add(current.MoveItemToFloor(generator1|generator2, potentialFloor));
                    }
                }
                foreach (var set in current.CurrentMatchingSets())
                {
                    nextStates.Add(current.MoveItemToFloor(set, potentialFloor));
                }
            }
            return nextStates.Where(state => state.IsValid()).ToList();
        }

        private static State ToInitialState(string[] lines, bool addExtraItems = false)
        {
            var floors = 0L;
            var names = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (var match in (IReadOnlyList<Match>)InputRegex().Matches(lines[i]))
                {
                    int shift;
                    if ((shift = names.IndexOf(match.Groups[1].Value)) == -1)
                    {
                        shift = names.Count;
                        names.Add(match.Groups[1].Value);
                    }
                    floors |= ((match.Groups[3].Value == "microchip") ? 1L : 2L) << (16 * (3 - i) + shift * 2);
                }
            }
            if (addExtraItems)
            {
                floors |= 0b1111L << 16 * 3 + names.Count * 2;
            }
            return new(0, floors);
        }

        [GeneratedRegex(@"([a-z]+)(\-compatible)? (generator|microchip)")]
        private static partial Regex InputRegex();

        // Every 16 bits is a floor (first set is floor 1, fourth set is floor 4)
        // Every  2 bits on a floor is a matching generator & microchip set
        // Generator = 10
        // Microchip = 01
        private const long _setMask = 0b11L;
        private const long _floorMask = 0b1111_1111_1111_1111L;
        private readonly record struct State(int CurrentFloor, long Floors)
        {
            public State MoveItemToFloor(long itemMask, int newFloor)
            {
                var floors = Floors;
                floors ^= itemMask << (16 * (3 - CurrentFloor));
                floors |= itemMask << (16 * (3 - newFloor));
                return new(newFloor, floors);
            }

            public bool IsValid()
            {
                for (int i = 0; i < 4; i++)
                {
                    var floor = Floors >> (16 * i) & _floorMask;
                    var chipWithoutGenerator = false;
                    var anyGenerators = false;
                    while (floor != 0L)
                    {
                        var set = floor & _setMask;
                        chipWithoutGenerator |= (set == 1);
                        anyGenerators |= (set > 1);
                        floor >>= 2;
                    }
                    if (chipWithoutGenerator && anyGenerators)
                    {
                        return false;
                    }
                }
                return true;
            }

            private IEnumerable<long> InnerGetItem(long mask)
            {
                var floor = Floors >> (16 * (3 - CurrentFloor)) & _floorMask;
                while (mask < _floorMask)
                {
                    if ((floor & mask) == mask)
                    {
                        yield return mask;
                    }
                    mask <<= 2;
                }
            }

            public IEnumerable<long> CurrentChips() => InnerGetItem(1L);
            public IEnumerable<long> CurrentGenerators() => InnerGetItem(2L);
            public IEnumerable<long> CurrentMatchingSets() => InnerGetItem(3L);
            public bool IsFinished() => Floors <= _floorMask;
        }

        private class BFS : Graphs.BFS<State>
        {
            protected override List<State> FindNeighbors(State node) => GetNextStates(node);
            protected override bool IsGoal(State node) => node.IsFinished();
        }
    }
}
