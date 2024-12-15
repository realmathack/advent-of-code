using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Solvers
{
    public partial class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var sum = 0;
            var cards = ToScratchCards(input);
            foreach (var card in cards)
            {
                var winningCount = card.Numbers.Intersect(card.WinningNumbers).Count();
                if (winningCount > 0)
                {
                    sum += (int)Math.Pow(2, winningCount - 1);
                }
            }
            return sum;
        }

        public override object SolvePart2(string[] input)
        {
            var tmp = ToScratchCards(input);
            tmp.Insert(0, new(0, [], [], 0));
            var cards = tmp.ToArray();
            for (int i = 1; i < cards.Length; i++)
            {
                var winningCount = cards[i].Numbers.Intersect(cards[i].WinningNumbers).Count();
                for (int j = 1; j <= winningCount; j++)
                {
                    cards[i + j].Count += cards[i].Count;
                }
            }
            return cards.Sum(card => card.Count);
        }

        private static List<ScratchCard> ToScratchCards(string[] lines)
        {
            var cards = new List<ScratchCard>();
            foreach (var line in lines)
            {
                var match = CardRegex().Match(line);
                var id = int.Parse(match.Groups[1].Value);
                var winning = match.Groups[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var numbers = match.Groups[3].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                cards.Add(new(id, winning, numbers, 1));
            }
            return cards;
        }

        [GeneratedRegex(@"Card\s+(\d+): (.+) \| (.+)")]
        private static partial Regex CardRegex();

        private record class ScratchCard(int Id, int[] WinningNumbers, int[] Numbers, int Count)
        {
            public int Count { get; set; } = Count;
        }
    }
}
