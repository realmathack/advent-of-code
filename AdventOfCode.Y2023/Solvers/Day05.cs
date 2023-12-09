namespace AdventOfCode.Y2023.Solvers
{
    public class Day05 : SolverWithSections
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
                var destination = new List<Int64Range>(ranges.Count);
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
                if (source >= mapping.Start && source <= mapping.End)
                {
                    destination += mapping.Offset;
                    break;
                }
            }
            return destination;
        }

        private static List<Int64Range> ToRanges(long[] seeds)
        {
            var ranges = new List<Int64Range>();
            for (int i = 0; i < seeds.Length; i += 2)
            {
                ranges.Add(new(seeds[i], seeds[i] + seeds[i + 1] - 1));
            }
            return ranges;
        }

        private static List<Int64Range> FindDestinationRanges(Map map, Int64Range source)
        {
            var ranges = new List<Int64Range>() { source };
            var destinations = new List<Int64Range>();
            foreach (var mapping in map.Mappings)
            {
                for (int i = ranges.Count - 1; i >= 0; i--)
                {
                    var range = ranges[i];
                    // Complete encapsulation
                    if (mapping.Start <= range.Start && mapping.End >= range.End)
                    {
                        destinations.Add(new(range.Start + mapping.Offset, range.End + mapping.Offset));
                        ranges.RemoveAt(i);
                    }
                    // Complete overlap
                    else if (mapping.Start >= range.Start && mapping.End <= range.End)
                    {
                        destinations.Add(new(mapping.Start + mapping.Offset, mapping.End + mapping.Offset));
                        if (mapping.Start != range.Start)
                        {
                            ranges.Add(new(range.Start, mapping.Start - 1));
                        }
                        if (mapping.End != range.End)
                        {
                            ranges.Add(new(mapping.End + 1, range.End));
                        }
                        ranges.RemoveAt(i);
                    }
                    // Partial overlap (start)
                    else if (mapping.Start >= range.Start && mapping.Start <= range.End)
                    {
                        destinations.Add(new(mapping.Start + mapping.Offset, range.End + mapping.Offset));
                        if (mapping.Start != range.Start)
                        {
                            ranges.Add(new(range.Start, mapping.Start - 1));
                        }
                        ranges.RemoveAt(i);
                    }
                    // Partial overlap (end)
                    else if (mapping.End >= range.Start && mapping.End <= range.End)
                    {
                        destinations.Add(new(range.Start + mapping.Offset, mapping.End + mapping.Offset));
                        if (mapping.End != range.End)
                        {
                            ranges.Add(new(mapping.End + 1, range.End));
                        }
                        ranges.RemoveAt(i);
                    }
                }
            }
            destinations.AddRange(ranges);
            return destinations;
        }

        private static readonly char[] _seperator = [' ', '-'];
        private static (long[] Seeds, Dictionary<string, Map> Maps) ToSeedsAndMaps(string[] input)
        {
            var seeds = input[0][7..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var maps = new Dictionary<string, Map>();
            for (int i = 1; i < input.Length; i++)
            {
                var lines = input[i].SplitIntoLines();
                var parts = lines[0].Split(_seperator, StringSplitOptions.RemoveEmptyEntries);
                var map = new Map(parts[0], parts[2]);
                maps[map.Source] = map;
                for (int j = 1; j < lines.Length; j++)
                {
                    var numbers = lines[j].Split(' ').Select(long.Parse).ToArray();
                    map.Mappings.Add(new(numbers[1], numbers[1] + numbers[2] - 1, numbers[0] - numbers[1]));
                }
            }
            return (seeds, maps);
        }

        private record class Map(string Source, string Destination)
        {
            public List<Mapping> Mappings { get; set; } = [];
        }

        private readonly record struct Mapping(long Start, long End, long Offset);
        private readonly record struct Int64Range(long Start, long End);
    }
}
