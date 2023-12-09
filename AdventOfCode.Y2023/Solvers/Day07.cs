namespace AdventOfCode.Y2023.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var hands = ToHands(input);
            var rank = 1L;
            return hands.Sum(x => x.Bet * rank++);
        }

        public override object SolvePart2(string[] input)
        {
            var hands = ToHands2(input);
            var rank = 1L;
            return hands.Sum(x => x.Bet * rank++);
        }

        private static readonly Dictionary<char, char> _mapping1 = new() { { 'A', 'Z' }, { 'K', 'Y' }, { 'Q', 'X' }, { 'J', 'W' }, { 'T', 'V' } };
        private static List<Hand> ToHands(string[] input)
        {
            var hands = new List<Hand>();
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                hands.Add(new(parts[0], ToRank(parts[0]), ToSortableHand(parts[0], _mapping1), int.Parse(parts[1])));
            }
            hands.Sort();
            return hands;
        }

        private static readonly Dictionary<char, char> _mapping2 = new() { { 'A', 'Z' }, { 'K', 'Y' }, { 'Q', 'X' }, { 'J', '1' }, { 'T', 'V' } };
        private static List<Hand> ToHands2(string[] input)
        {
            var hands = new List<Hand>();
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                var groups = parts[0].GroupBy(x => x).Select(g => new { g.Key, Count = g.Count() }).ToDictionary(x => x.Key, x => x.Count);
                var jokers = groups.Remove('J', out var tmp) ? tmp : 0;
                var newCard = (jokers == 5 || jokers == 0) ? 'J' : (groups.Any(x => x.Value >= 2) ? groups.First(x => x.Value >= 2) : groups.First()).Key;
                var rankingHand = parts[0].Replace('J', newCard);
                hands.Add(new(parts[0], ToRank(rankingHand), ToSortableHand(parts[0], _mapping2), int.Parse(parts[1])));
            }
            hands.Sort();
            return hands;
        }

        private static string ToSortableHand(string hand, Dictionary<char, char> mapping)
        {
            return new string(hand.Select(x => mapping.TryGetValue(x, out var r) ? r : x).ToArray());
        }

        private static int ToRank(string hand)
        {
            var groups = hand.GroupBy(x => x).Select(g => g.Count()).OrderByDescending(x => x).ToList();
            return groups[0] switch
            {
                5 => 7,
                4 => 6,
                3 => groups.Count == 2 ? 5 : 4,
                2 => groups.Count == 3 ? 3 : 2,
                _ => 1
            };
        }

        private class Hand(string hand, int rank, string comparebleHand, int bet) : IComparable<Hand>
        {
            private readonly string _comparebleHand = comparebleHand;
            private readonly int _rank = rank;

            public string OriginalHand { get; init; } = hand;
            public int Bet { get; init; } = bet;

            public int CompareTo(Hand? other)
            {
                if (other is null)
                {
                    return 1;
                }
                if (_rank == other._rank)
                {
                    for (int i = 0; i < _comparebleHand.Length; i++)
                    {
                        if (_comparebleHand[i] == other._comparebleHand[i])
                        {
                            continue;
                        }
                        return _comparebleHand[i] > other._comparebleHand[i] ? 1 : -1;
                    }
                    return 0;
                }
                return _rank > other._rank ? 1 : -1;
            }
        }
    }
}
