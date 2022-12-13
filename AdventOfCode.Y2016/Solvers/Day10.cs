namespace AdventOfCode.Y2016.Solvers
{
    public class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return Solve(input);
        }

        public override object SolvePart2(string[] input)
        {
            return Solve(input, true);
        }

        private static int Solve(string[] input, bool p2 = false)
        {
            var outputs = new Dictionary<int, int>();
            var bots = new Dictionary<int, Bot>();
            foreach (var line in input.OrderBy(x => x))
            {
                var parts = line.Split(' ');
                if (parts[0] == "value")
                {
                    bots[int.Parse(parts[5])].Values.Add(int.Parse(parts[1]));
                    continue;
                }
                var number = int.Parse(parts[1]);
                bots.Add(number, new Bot(number, new(), int.Parse(parts[6]), parts[5] == "bot", int.Parse(parts[11]), parts[10] == "bot"));
            }
            while (true)
            {
                var bot = bots.FirstOrDefault(x => x.Value.Values.Count == 2).Value;
                if (p2 && bot is null)
                {
                    return outputs[0] * outputs[1] * outputs[2];
                }
                if (!p2 && bot.Values.Contains(61) && bot.Values.Contains(17))
                {
                    return bot.Number;
                }
                if (bot.IsLowDestinationBot)
                {
                    bots[bot.LowDestinationNumber].Values.Add(bot.Values.Min());
                }
                else
                {
                    outputs.SetOrAdd(bot.LowDestinationNumber, bot.Values.Min());
                }
                if (bot.IsHighDestinationBot)
                {
                    bots[bot.HighDestinationNumber].Values.Add(bot.Values.Max());
                }
                else
                {
                    outputs.SetOrAdd(bot.HighDestinationNumber, bot.Values.Max());
                }
                bot.Values.Clear();
            }
        }

        private record class Bot(int Number, List<int> Values, int LowDestinationNumber, bool IsLowDestinationBot, int HighDestinationNumber, bool IsHighDestinationBot);
    }
}
