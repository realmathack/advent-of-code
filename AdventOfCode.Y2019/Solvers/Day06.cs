﻿namespace AdventOfCode.Y2019.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var orbits = ToOrbits(input);
            var total = 0;
            var depth = 0;
            var sortedByDepth = new Dictionary<int, List<string>>() { [0] = ["COM"] };
            while (orbits.Count > 0)
            {
                var tmp = sortedByDepth[depth].Where(orbits.ContainsKey).SelectMany(obj => orbits[obj]).ToList();
                sortedByDepth[depth].ForEach(obj => orbits.Remove(obj));
                sortedByDepth[++depth] = tmp;
                total += tmp.Count * depth;
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var orbits = ToOrbits(input);
            var start = orbits.Single(orbit => orbit.Value.Contains("YOU")).Key;
            var goal = orbits.Single(orbit => orbit.Value.Contains("SAN")).Key;
            var depth = 0;
            var current = new HashSet<string>() { start };
            while (!current.Contains(goal))
            {
                var nextObjects = orbits
                    .Where(orbit => current.Contains(orbit.Key))
                    .SelectMany(orbit => orbit.Value)
                    .Where(obj => !current.Contains(obj))
                    .ToArray();
                var nextCenters = orbits
                    .Where(orbit => orbit.Value.Any(obj => current.Contains(obj)))
                    .Select(orbit => orbit.Key)
                    .Where(center => !current.Contains(center))
                    .ToArray();
                current = [.. nextObjects, .. nextCenters];
                depth++;
            }
            return depth;
        }

        private static Dictionary<string, HashSet<string>> ToOrbits(string[] lines)
        {
            return lines
                .Select(line => line.SplitInTwo(')'))
                .GroupBy(orbit => orbit.Left)
                .Select(g => (g.Key, Value: g.Select(orbit => orbit.Right).ToHashSet()))
                .ToDictionary();
        }
    }
}
