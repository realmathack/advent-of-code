namespace AdventOfCode.Y2018.Solvers
{
    public class Day09 : SolverWithText
    {
        public override object SolvePart1(string input) => Solve(input);

        public override object SolvePart2(string input) => Solve(input, true);

        private static long Solve(string text, bool timesHundred = false)
        {
            var (playerCount, last) = ToPlayersAndMarbles(text);
            if (timesHundred)
            {
                last *= 100;
            }
            var players = new long[playerCount];
            var circle = new LinkedList<int>();
            var current = circle.AddFirst(0);
            for (int marble = 1; marble <= last; marble++)
            {
                if (marble % 23 == 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        current = current?.Previous ?? circle.Last;
                    }
                    if (current is null)
                    {
                        throw new InvalidOperationException();
                    }
                    players[(marble - 1) % playerCount] += marble + current.Value;
                    var tmp = current.Next;
                    circle.Remove(current);
                    current = tmp;
                    continue;
                }
                current = current?.Next ?? circle.First;
                if (current is null)
                {
                    throw new InvalidOperationException();
                }
                current = circle.AddAfter(current, marble);
            }
            return players.Max();
        }

        private static (int PlayerCount, int LastMarble) ToPlayersAndMarbles(string text)
        {
            var parts = text.Split(' ');
            return (int.Parse(parts[0]), int.Parse(parts[6]));
        }
    }
}
