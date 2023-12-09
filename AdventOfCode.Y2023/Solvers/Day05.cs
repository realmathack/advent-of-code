
namespace AdventOfCode.Y2023.Solvers
{
    public class Day05 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var (source, targets) = ToSeedsAndTargets(input);
            var current = "seed";
            while (targets.TryGetValue(current, out var target))
            {
                var destination = new long[source.Length];
                for (int i = 0; i < source.Length; i++)
                {
                    destination[i] = GetDestination(target, source[i]);
                }
                source = destination;
                current = target.Destination;
            }
            return source.Min();
        }

        public override object SolvePart2(string[] input)
        {
            var (source, targets) = ToSeedsAndTargets(input);
            var ranges = ToRanges(source);
            var current = "seed";
            while (targets.TryGetValue(current, out var target))
            {
                var destination = new List<LongRange>(ranges.Count);
                foreach (var range in ranges)
                {
                    destination.AddRange(GetDestinations(target, range));
                }
                ranges = destination;
                current = target.Destination;
            }
            return ranges.Min(range => range.First);
        }

        private static long GetDestination(Target target, long source)
        {
            var destination = source;
            foreach (var mapping in target.Mappings)
            {
                if (source >= mapping.First && source <= mapping.Last)
                {
                    destination += mapping.Offset;
                    break;
                }
            }
            return destination;
        }

        private static List<LongRange> ToRanges(long[] seeds)
        {
            var ranges = new List<LongRange>();
            for (int i = 0; i < seeds.Length; i += 2)
            {
                ranges.Add(new(seeds[i], seeds[i] + seeds[i + 1] - 1));
            }
            return ranges;
        }

        private static List<LongRange> GetDestinations(Target target, LongRange source)
        {
            var ranges = new List<LongRange>() { source };
            var destinations = new List<LongRange>();
            foreach (var mapping in target.Mappings)
            {
                for (int i = ranges.Count - 1; i >= 0; i--)
                {
                    var range = ranges[i];
                    // Complete encapsulation
                    if (mapping.First <= range.First && mapping.Last >= range.Last)
                    {
                        destinations.Add(new(range.First + mapping.Offset, range.Last + mapping.Offset));
                        ranges.RemoveAt(i);
                    }
                    // Complete overlap
                    else if (mapping.First >= range.First && mapping.Last <= range.Last)
                    {
                        destinations.Add(new(mapping.First + mapping.Offset, mapping.Last + mapping.Offset));
                        if (mapping.First != range.First)
                        {
                            ranges.Add(new(range.First, mapping.First - 1));
                        }
                        if (mapping.Last != range.Last)
                        {
                            ranges.Add(new(mapping.Last + 1, range.Last));
                        }
                        ranges.RemoveAt(i);
                    }
                    // Partial overlap (start)
                    else if (mapping.First >= range.First && mapping.First <= range.Last)
                    {
                        destinations.Add(new(mapping.First + mapping.Offset, range.Last + mapping.Offset));
                        if (mapping.First != range.First)
                        {
                            ranges.Add(new(range.First, mapping.First - 1));
                        }
                        ranges.RemoveAt(i);
                    }
                    // Partial overlap (end)
                    else if (mapping.Last >= range.First && mapping.Last <= range.Last)
                    {
                        destinations.Add(new(range.First + mapping.Offset, mapping.Last + mapping.Offset));
                        if (mapping.Last != range.Last)
                        {
                            ranges.Add(new(mapping.Last + 1, range.Last));
                        }
                        ranges.RemoveAt(i);
                    }
                }
            }
            destinations.AddRange(ranges);
            return destinations;
        }

        private static readonly char[] _seperator = [' ', '-'];
        private static (long[] Seeds, Dictionary<string, Target> Targets) ToSeedsAndTargets(string[] input)
        {
            var seeds = input[0][7..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var targets = new Dictionary<string, Target>();
            for (int i = 1; i < input.Length; i++)
            {
                var lines = input[i].SplitIntoLines();
                var parts = lines[0].Split(_seperator, StringSplitOptions.RemoveEmptyEntries);
                var target = new Target(parts[0], parts[2]);
                targets[target.Source] = target;
                for (int j = 1; j < lines.Length; j++)
                {
                    var numbers = lines[j].Split(' ').Select(long.Parse).ToArray();
                    target.Mappings.Add(new(numbers[1], numbers[1] + numbers[2] - 1, numbers[0] - numbers[1]));
                }
            }
            return (seeds, targets);
        }

        private record class Target(string Source, string Destination)
        {
            public List<Mapping> Mappings { get; set; } = [];
        }

        private record struct Mapping(long First, long Last, long Offset);

        private struct LongRange(long first, long last)
        {
            public long First { get; set; } = first;
            public long Last { get; set; } = last;
        }
    }
}
