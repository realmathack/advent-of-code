using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public partial class Day14 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToReindeers(input).Max(reindeer => Fly(reindeer, 2503));
        public override object SolvePart2(string[] input) => FindHighestScoreAtSeconds(ToReindeers(input), 2503);

        private static List<Reindeer> ToReindeers(string[] lines)
        {
            var reindeers = new List<Reindeer>();
            foreach (var line in lines)
            {
                var match = ReindeerRegex().Match(line);
                reindeers.Add(new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)));
            }
            return reindeers;
        }

        private static int Fly(Reindeer reindeer, int seconds)
        {
            var fullSeconds = reindeer.FlySeconds + reindeer.RestSeconds;
            var flightSeconds = (seconds / fullSeconds) * reindeer.FlySeconds;
            flightSeconds += Math.Min(reindeer.FlySeconds, seconds % fullSeconds);
            return flightSeconds * reindeer.Speed;
        }

        private static int FindHighestScoreAtSeconds(List<Reindeer> reindeers, int seconds)
        {
            var scores = reindeers.ToDictionary(reindeer => reindeer, score => 0);
            var distances = reindeers.ToDictionary(reindeer => reindeer, distance => 0);
            for (int i = 0; i < seconds; i++)
            {
                foreach (var reindeer in reindeers)
                {
                    if (i % (reindeer.FlySeconds + reindeer.RestSeconds) < reindeer.FlySeconds)
                    {
                        distances[reindeer] += reindeer.Speed;
                    }
                }
                var topDistance = distances.Max(reindeer => reindeer.Value);
                foreach (var reindeer in distances.Where(reindeer => reindeer.Value == topDistance).Select(reindeer => reindeer.Key))
                {
                    scores[reindeer]++;
                }
            }
            return scores.Values.Max();
        }

        [GeneratedRegex(@".+ can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds\.")]
        private static partial Regex ReindeerRegex();

        private record class Reindeer(int Speed, int FlySeconds, int RestSeconds);
    }
}
