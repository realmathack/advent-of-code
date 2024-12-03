namespace AdventOfCode.Y2023.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var hands = ToHands(input);
            var rank = 1L;
            return hands.Sum(hand => hand.Bet * rank++);
        }

        public override object SolvePart2(string[] input)
        {
            var hands = ToHands2(input);
            var rank = 1L;
            return hands.Sum(hand => hand.Bet * rank++);
        }

        private static readonly Dictionary<char, char> _mapping1 = new() { ['A'] = 'Z', ['K'] = 'Y', ['Q'] = 'X', ['J'] = 'W', ['T'] = 'V' };
        private static List<Hand> ToHands(string[] lines)
        {
            var hands = new List<Hand>();
            foreach (var line in lines)
            {
                var (hand, bet) = line.SplitInTwo(' ');
                hands.Add(new(hand, ToRank(hand), ToSortableHand(hand, _mapping1), int.Parse(bet)));
            }
            hands.Sort();
            return hands;
        }

        private static readonly Dictionary<char, char> _mapping2 = new() { ['A'] = 'Z', ['K'] = 'Y', ['Q'] = 'X', ['J'] = '1', ['T'] = 'V' };
        private static List<Hand> ToHands2(string[] lines)
        {
            var hands = new List<Hand>();
            foreach (var line in lines)
            {
                var (hand, bet) = line.SplitInTwo(' ');
                var cards = hand.GroupBy(card => card).Select(g => (g.Key, Count: g.Count())).ToDictionary();
                var jokers = cards.Remove('J', out var tmp) ? tmp : 0;
                var newCard = (jokers == 5 || jokers == 0) ? 'J' : (cards.Any(card => card.Value >= 2) ? cards.First(card => card.Value >= 2) : cards.First()).Key;
                var rankingHand = hand.Replace('J', newCard);
                hands.Add(new(hand, ToRank(rankingHand), ToSortableHand(hand, _mapping2), int.Parse(bet)));
            }
            hands.Sort();
            return hands;
        }

        private static string ToSortableHand(string hand, Dictionary<char, char> mapping)
        {
            return string.Concat(hand.Select(card => mapping.TryGetValue(card, out var mapped) ? mapped : card));
        }

        private static int ToRank(string hand)
        {
            var groups = hand.GroupBy(card => card).Select(g => g.Count()).OrderByDescending(count => count).ToList();
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
