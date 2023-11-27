using System;

namespace AdventOfCode.Y2022.Solvers
{
    public class Day15 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => SolvePart1(input, 2000000);
        public int SolvePart1(string[] input, int targetRow)
        {
            var blocked = new HashSet<Coords>();
            var beacons = ToBeacons(input);
            foreach (var beacon in beacons)
            {
                var distance = beacon.Key.DistanceTo(beacon.Value);
                var distanceY = Math.Abs(targetRow - beacon.Key.Y);
                if (distanceY <= distance)
                {
                    var center = new Coords(beacon.Key.X, targetRow);
                    blocked.Add(center);
                    for (int i = 1; i <= distance - distanceY; i++)
                    {
                        blocked.Add(new(center.X - i, center.Y));
                        blocked.Add(new(center.X + i, center.Y));
                    }
                }
            }
            foreach (var beacon in beacons.Values)
            {
                blocked.Remove(beacon);
            }
            return blocked.Count;
        }

        public override object SolvePart2(string[] input) => SolvePart2(input, 4000000);
        public long SolvePart2(string[] input, int maxXY)
        {
            var beacons = ToBeacons(input);
            var remaining = new Dictionary<int, List<(int begin, int end)>>();
            for (int i = 0; i <= maxXY; i++)
            {
                remaining.Add(i, new() { (0, maxXY) });
            }
            foreach (var beacon in beacons)
            {
                var distance = beacon.Key.DistanceTo(beacon.Value);
                for (int i = 0; i <= distance; i++)
                {
                    ProcessRow(beacon.Key.Y - i, remaining, beacon.Key, distance - i, maxXY);
                    if (i == 0) continue;
                    ProcessRow(beacon.Key.Y + i, remaining, beacon.Key, distance - i, maxXY);
                }
            }
            if (remaining.Count != 1 || remaining.First().Value.Count != 1 || remaining.First().Value.First().begin != remaining.First().Value.First().end)
            {
                throw new InvalidOperationException("Remaining bigger than 1");
            }
            return remaining.First().Value.First().begin * 4000000L + remaining.First().Key;
        }

        private static void ProcessRow(int row, Dictionary<int, List<(int begin, int end)>> remaining, Coords beacon, int distanceX, int maxXY)
        {
            if (!remaining.TryGetValue(row, out var ranges))
            {
                return;
            }
            var currentRange = (begin: beacon.X - distanceX, end: beacon.X + distanceX);
            var affectedRanges = new List<(int begin, int end)>();
            for (int i = ranges.Count - 1; i >= 0; i--)
            {
                if (FullOverlap(ranges[i], currentRange))
                {
                    ranges.RemoveAt(i);
                }
                else if (AnyOverlap(ranges[i], currentRange))
                {
                    affectedRanges.Add(ranges[i]);
                    ranges.RemoveAt(i);
                }
            }
            if (currentRange.begin >= 0)
            {
                foreach (var (begin, end) in affectedRanges.Where(x => x.begin < currentRange.begin))
                {
                    ranges.Add(new(begin, currentRange.begin - 1));
                }
            }
            if (currentRange.end <= maxXY)
            {
                foreach (var (begin, end) in affectedRanges.Where(x => x.end > currentRange.end))
                {
                    ranges.Add(new(currentRange.end + 1, end));
                }
            }
            if (ranges.Count == 0)
            {
                remaining.Remove(row);
            }
        }

        private static bool FullOverlap((int begin, int end) old, (int begin, int end) current) => old.begin >= current.begin && current.end >= old.end;
        private static bool AnyOverlap((int begin, int end) old, (int begin, int end) current) => old.begin <= current.end && current.begin <= old.end;

        private static Dictionary<Coords, Coords> ToBeacons(string[] input)
        {
            var beacons = new Dictionary<Coords, Coords>();
            foreach (var line in input)
            {
                var parts = line.Split(new[] { ' ', ',', ':', '=' }, StringSplitOptions.RemoveEmptyEntries);
                beacons.Add(new(int.Parse(parts[3]), int.Parse(parts[5])), new(int.Parse(parts[11]), int.Parse(parts[13])));
            }
            return beacons;
        }
    }
}
