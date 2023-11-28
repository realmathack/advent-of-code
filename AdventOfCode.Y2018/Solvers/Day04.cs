namespace AdventOfCode.Y2018.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            input = [.. input.OrderBy(x => x)];
            var mostAsleep = ToSleeps(input)
                .GroupBy(s => s.Id)
                .Select(g => new { Id = g.Key, Minutes = g.SelectMany(s => s.Minutes) })
                .OrderByDescending(x => x.Minutes.Count())
                .First();
            var mostFrequentMinute = mostAsleep.Minutes
                .GroupBy(m => m)
                .Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .First().Key;
            return mostAsleep.Id * mostFrequentMinute;
        }

        public override object SolvePart2(string[] input)
        {
            input = [.. input.OrderBy(x => x)];
            var mostFrequentAsleep = ToSleeps(input)
                .GroupBy(s => s.Id)
                .Select(g => new { Id = g.Key, MostFrequentMinute = g.SelectMany(s => s.Minutes)
                    .GroupBy(s => s)
                    .Select(g => new { Minute = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .First()
                })
                .OrderByDescending(x => x.MostFrequentMinute.Count)
                .First();
            return mostFrequentAsleep.Id * mostFrequentAsleep.MostFrequentMinute.Minute;
        }

        private static List<Sleep> ToSleeps(string[] input)
        {
            var sleeps = new List<Sleep>();
            var id = 0;
            int start = 0;
            foreach (var line in input)
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

        private record class Sleep(int Id, int[] Minutes);
    }
}
