namespace AdventOfCode.Y2022.Solvers
{
    public class Day15(int givenNumber) : SolverWithLines
    {
        public Day15() : this(2000000) { }

        public override object SolvePart1(string[] input)
        {
            int targetRow = givenNumber;
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

        public override object SolvePart2(string[] input)
        {
            int maxXY = givenNumber * 2;
            var beacons = ToBeacons(input);
            var remaining = new Dictionary<int, List<(int Begin, int End)>>();
            for (int i = 0; i <= maxXY; i++)
            {
                remaining.Add(i, [(0, maxXY)]);
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
            if (remaining.Count != 1 || remaining.First().Value.Count != 1 || remaining.First().Value.First().Begin != remaining.First().Value.First().End)
            {
                throw new InvalidOperationException("Remaining bigger than 1");
            }
            return remaining.First().Value.First().Begin * 4000000L + remaining.First().Key;
        }

        private static void ProcessRow(int row, Dictionary<int, List<(int Begin, int End)>> remaining, Coords beacon, int distanceX, int maxXY)
        {
            if (!remaining.TryGetValue(row, out var ranges))
            {
                return;
            }
            var currentRange = (Begin: beacon.X - distanceX, End: beacon.X + distanceX);
            var affectedRanges = new List<(int Begin, int End)>();
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
            if (currentRange.Begin >= 0)
            {
                foreach (var (begin, end) in affectedRanges.Where(range => range.Begin < currentRange.Begin))
                {
                    ranges.Add(new(begin, currentRange.Begin - 1));
                }
            }
            if (currentRange.End <= maxXY)
            {
                foreach (var (begin, end) in affectedRanges.Where(range => range.End > currentRange.End))
                {
                    ranges.Add(new(currentRange.End + 1, end));
                }
            }
            if (ranges.Count == 0)
            {
                remaining.Remove(row);
            }
        }

        private static bool FullOverlap((int Begin, int End) old, (int Begin, int End) current) => old.Begin >= current.Begin && current.End >= old.End;
        private static bool AnyOverlap((int Begin, int End) old, (int Begin, int End) current) => old.Begin <= current.End && current.Begin <= old.End;

        private static readonly char[] _separator = [' ', ',', ':', '='];
        private static Dictionary<Coords, Coords> ToBeacons(string[] input)
        {
            var beacons = new Dictionary<Coords, Coords>();
            foreach (var line in input)
            {
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                beacons.Add(new(int.Parse(parts[3]), int.Parse(parts[5])), new(int.Parse(parts[11]), int.Parse(parts[13])));
            }
            return beacons;
        }
    }
}
