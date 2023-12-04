namespace AdventOfCode.Y2023.Solvers
{
    public class Day04 : SolverWithLines
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
            tmp.Insert(0, new(0, [], []) { Count = 0 });
            var cards = tmp.ToArray();
            for (int i = 1; i < cards.Length; i++)
            {
                var winningCount = cards[i].Numbers.Intersect(cards[i].WinningNumbers).Count();
                for (int j = 1; j <= winningCount; j++)
                {
                    cards[i + j].Count += cards[i].Count;
                }
            }
            return cards.Sum(x => x.Count);
        }

        private static List<ScratchCard> ToScratchCards(string[] input)
        {
            var cards = new List<ScratchCard>();
            foreach (var line in input)
            {
                var parts = line[5..].Split(':', StringSplitOptions.TrimEntries);
                var id = int.Parse(parts[0]);
                parts = parts[1].Split(" | ", StringSplitOptions.RemoveEmptyEntries);
                var winning = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var numbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                cards.Add(new(id, winning, numbers));
            }
            return cards;
        }

        private record class ScratchCard(int Id, int[] WinningNumbers, int[] Numbers)
        {
            public int Count { get; set; } = 1;
        }
    }
}
