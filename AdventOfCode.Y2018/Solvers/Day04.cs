namespace AdventOfCode.Y2018.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            input = [.. input.OrderBy(line => line)];
            var (mostAsleepId, mostAsleepMinutes) = ToSleeps(input)
                .GroupBy(sleep => sleep.Id)
                .Select(g => (Id: g.Key, Minutes: g.SelectMany(sleep => sleep.Minutes)))
                .OrderByDescending(g => g.Minutes.Count())
                .First();
            var mostFrequentMinute = mostAsleepMinutes
                .GroupBy(minute => minute)
                .Select(g => (g.Key, Count: g.Count()))
                .OrderByDescending(g => g.Count)
                .First().Key;
            return mostAsleepId * mostFrequentMinute;
        }

        public override object SolvePart2(string[] input)
        {
            input = [.. input.OrderBy(line => line)];
            var (mostFrequentAsleepId, mostFrequentMinute) = ToSleeps(input)
                .GroupBy(sleep => sleep.Id)
                .Select(g => (Id: g.Key, MostFrequentMinute: g
                    .SelectMany(sleep => sleep.Minutes)
                    .GroupBy(minute => minute)
                    .Select(g => (Minute: g.Key, Count: g.Count()))
                    .OrderByDescending(g => g.Count)
                    .First()))
                .OrderByDescending(g => g.MostFrequentMinute.Count)
                .First();
            return mostFrequentAsleepId * mostFrequentMinute.Minute;
        }

        private static List<(int Id, int[] Minutes)> ToSleeps(string[] lines)
        {
            var sleeps = new List<(int Id, int[] Minutes)>();
            var id = 0;
            var start = 0;
            foreach (var line in lines)
            {
                var minute = int.Parse(line[15..17]);
                var what = line[25..];
                if (what == "up")
                {
                    sleeps.Add(new(id, [.. Enumerable.Range(start, minute - start)]));
                }
                else if (what == "asleep")
                {
                    start = minute;
                }
                else
                {
                    id = int.Parse(what[1..what.IndexOf(' ')]);
                }
            }
            return sleeps;
        }
    }
}
