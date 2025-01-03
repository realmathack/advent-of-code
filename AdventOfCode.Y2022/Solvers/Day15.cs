﻿using Coords = AdventOfCode.Coords<int>;
using Range = AdventOfCode.Range<int>;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2022.Solvers
{
    public partial class Day15(int _givenNumber) : SolverWithLines
    {
        public Day15() : this(2_000_000) { }

        public override object SolvePart1(string[] input)
        {
            int targetRow = _givenNumber;
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
            int maxXY = _givenNumber * 2;
            var beacons = ToBeacons(input);
            var remaining = new Dictionary<int, List<Range>>();
            for (int i = 0; i <= maxXY; i++)
            {
                remaining.Add(i, [new(0, maxXY)]);
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
            if (remaining.Count != 1 || remaining.First().Value.Count != 1 || remaining.First().Value.First().Start != remaining.First().Value.First().End)
            {
                throw new InvalidOperationException("Remaining bigger than 1");
            }
            return remaining.First().Value.First().Start * 4_000_000L + remaining.First().Key;
        }

        private static void ProcessRow(int row, Dictionary<int, List<Range>> remaining, Coords beacon, int distanceX, int maxXY)
        {
            if (!remaining.TryGetValue(row, out var ranges))
            {
                return;
            }
            var currentRange = new Range(beacon.X - distanceX, beacon.X + distanceX);
            var affectedRanges = new List<Range>();
            for (int i = ranges.Count - 1; i >= 0; i--)
            {
                if (currentRange.FullyOverlapsWith(ranges[i]))
                {
                    ranges.RemoveAt(i);
                }
                else if (currentRange.HasAnyOverlapWith(ranges[i]))
                {
                    affectedRanges.Add(ranges[i]);
                    ranges.RemoveAt(i);
                }
            }
            if (currentRange.Start >= 0)
            {
                foreach (var (start, end) in affectedRanges.Where(range => range.Start < currentRange.Start))
                {
                    ranges.Add(new(start, currentRange.Start - 1));
                }
            }
            if (currentRange.End <= maxXY)
            {
                foreach (var (start, end) in affectedRanges.Where(range => range.End > currentRange.End))
                {
                    ranges.Add(new(currentRange.End + 1, end));
                }
            }
            if (ranges.Count == 0)
            {
                remaining.Remove(row);
            }
        }

        private static Dictionary<Coords, Coords> ToBeacons(string[] lines)
        {
            var beacons = new Dictionary<Coords, Coords>();
            foreach (var line in lines)
            {
                var match = BeaconRegex().Match(line);
                beacons.Add(new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)), new(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)));
            }
            return beacons;
        }

        [GeneratedRegex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)")]
        private static partial Regex BeaconRegex();
    }
}
