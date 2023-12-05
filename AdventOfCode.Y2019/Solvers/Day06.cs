namespace AdventOfCode.Y2019.Solvers
{
    public class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var orbits = input.Select(x => x.Split(')')).Select(x => new { Center = x[0], Orbits = x[1] })
                .GroupBy(x => x.Center).Select(g => new { g.Key, Value = g.Select(x => x.Orbits).ToList() }).ToDictionary(x => x.Key, x => x.Value);
            var total = 0;
            var depth = 0;
            var sortedByDepth = new Dictionary<int, List<string>>() { { 0, ["COM"] } };
            while (orbits.Count > 0)
            {
                var tmp = sortedByDepth[depth].Where(orbits.ContainsKey).SelectMany(x => orbits[x]).ToList();
                sortedByDepth[depth].ForEach(x => orbits.Remove(x));
                sortedByDepth[++depth] = tmp;
                total += tmp.Count * depth;
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var orbits = input.Select(x => x.Split(')')).Select(x => new { Center = x[0], Orbits = x[1] })
                .GroupBy(x => x.Center).Select(g => new { g.Key, Value = g.Select(x => x.Orbits).ToList() }).ToDictionary(x => x.Key, x => x.Value);
            var source = orbits.Single(x => x.Value.Contains("YOU")).Key;
            var target = orbits.Single(x => x.Value.Contains("SAN")).Key;
            // TODO: Implement (basically pathfinding)
            return null!;
        }
    }
}