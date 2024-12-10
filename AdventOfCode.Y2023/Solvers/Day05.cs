using Range = AdventOfCode.Range<long>;

namespace AdventOfCode.Y2023.Solvers
{
    public class Day05 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var (source, maps) = ToSeedsAndMaps(input);
            var current = "seed";
            while (maps.TryGetValue(current, out var map))
            {
                var destination = new long[source.Length];
                for (int i = 0; i < source.Length; i++)
                {
                    destination[i] = FindDestination(map, source[i]);
                }
                source = destination;
                current = map.Destination;
            }
            return source.Min();
        }

        public override object SolvePart2(string[] input)
        {
            var (source, maps) = ToSeedsAndMaps(input);
            var ranges = ToRanges(source);
            var current = "seed";
            while (maps.TryGetValue(current, out var map))
            {
                var destination = new List<Range>(ranges.Count);
                foreach (var range in ranges)
                {
                    destination.AddRange(FindDestinationRanges(map, range));
                }
                ranges = destination;
                current = map.Destination;
            }
            return ranges.Min(range => range.Start);
        }

        private static long FindDestination(Map map, long source)
        {
            var destination = source;
            foreach (var mapping in map.Mappings)
            {
                if (source >= mapping.Range.Start && source <= mapping.Range.End)
                {
                    destination += mapping.Offset;
                    break;
                }
            }
            return destination;
        }

        private static List<Range> ToRanges(long[] seeds)
        {
            var ranges = new List<Range>();
            for (int i = 0; i < seeds.Length; i += 2)
            {
                ranges.Add(new(seeds[i], seeds[i] + seeds[i + 1] - 1));
            }
            return ranges;
        }

        private static List<Range> FindDestinationRanges(Map map, Range source)
        {
            var ranges = new List<Range>() { source };
            var destinations = new List<Range>();
            foreach (var mapping in map.Mappings)
            {
                for (int i = ranges.Count - 1; i >= 0; i--)
                {
                    var range = ranges[i];
                    if (mapping.Range.FullyOverlapsWith(range))
                    {
                        destinations.Add(range + mapping.Offset);
                    }
                    else if (mapping.Range.IsFullyEnclosedBy(range))
                    {
                        destinations.Add(mapping.Range + mapping.Offset);
                    }
                    else if (mapping.Range.StartOverlapsWith(range))
                    {
                        destinations.Add(new(mapping.Range.Start + mapping.Offset, range.End + mapping.Offset));
                    }
                    else if (mapping.Range.EndOverlapsWith(range))
                    {
                        destinations.Add(new(range.Start + mapping.Offset, mapping.Range.End + mapping.Offset));
                    }
                    else
                    {
                        continue;
                    }
                    if (mapping.Range.Start > range.Start)
                    {
                        ranges.Add(new(range.Start, mapping.Range.Start - 1));
                    }
                    if (mapping.Range.End < range.End)
                    {
                        ranges.Add(new(mapping.Range.End + 1, range.End));
                    }
                    ranges.RemoveAt(i);
                }
            }
            destinations.AddRange(ranges);
            return destinations;
        }

        private static readonly char[] _seperator = [' ', '-'];
        private static (long[] Seeds, Dictionary<string, Map> Maps) ToSeedsAndMaps(string[] lineGroups)
        {
            var seeds = lineGroups[0][7..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var maps = new Dictionary<string, Map>();
            for (int i = 1; i < lineGroups.Length; i++)
            {
                var lines = lineGroups[i].SplitIntoLines();
                var parts = lines[0].Split(_seperator, StringSplitOptions.RemoveEmptyEntries);
                var map = new Map(parts[0], parts[2]);
                maps[map.Source] = map;
                for (int j = 1; j < lines.Length; j++)
                {
                    var numbers = lines[j].Split(' ').Select(long.Parse).ToArray();
                    map.Mappings.Add(new(new(numbers[1], numbers[1] + numbers[2] - 1), numbers[0] - numbers[1]));
                }
            }
            return (seeds, maps);
        }

        private record class Map(string Source, string Destination)
        {
            public List<Mapping> Mappings { get; set; } = [];
        }

        private record class Mapping(Range Range, long Offset);
    }
}
