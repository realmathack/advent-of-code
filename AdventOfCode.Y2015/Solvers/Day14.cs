namespace AdventOfCode.Y2015.Solvers
{
    public class Day14 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToReindeers(input).Max(reindeer => Fly(reindeer, 2503));
        public override object SolvePart2(string[] input) => FindHighestScoreAtSeconds(ToReindeers(input), 2503);

        private static List<Reindeer> ToReindeers(string[] lines)
        {
            var reindeers = new List<Reindeer>();
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                reindeers.Add(new(int.Parse(parts[3]), int.Parse(parts[6]), int.Parse(parts[13])));
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

        private readonly record struct Reindeer(int Speed, int FlySeconds, int RestSeconds);
    }
}
