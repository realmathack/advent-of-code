using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day08 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid) => FindUniqueAntinodesCount(grid, CalculateAntinodesPart1);
        public override object SolvePart2(char[][] grid) => FindUniqueAntinodesCount(grid, CalculateAntinodesPart2);

        private static int FindUniqueAntinodesCount(char[][] grid, Func<char[][], Coords, Coords, List<Coords>> calculateAntinodes)
        {
            var groupedAntennas = ToGroupedAntennas(grid);
            var antinondes = new HashSet<Coords>();
            foreach (var group in groupedAntennas)
            {
                antinondes.UnionWith(GetUniqueAntinodesForGroup(grid, group.Value, calculateAntinodes));
            }
            return antinondes.Count;
        }

        private static HashSet<Coords> GetUniqueAntinodesForGroup(char[][] grid, List<Coords> antennas, Func<char[][], Coords, Coords, List<Coords>> calculateAntinodes)
        {
            var antinondes = new HashSet<Coords>();
            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    foreach (var antinode in calculateAntinodes(grid, antennas[i], antennas[j]))
                    {
                        antinondes.Add(antinode);
                    }
                }
            }
            return antinondes;
        }

        private static List<Coords> CalculateAntinodesPart1(char[][] grid, Coords first, Coords second)
        {
            var antinodes = new List<Coords>(2);
            var offset = second - first;
            var potential = first - offset;
            if (!grid.IsOutOfBounds(potential))
            {
                antinodes.Add(potential);
            }
            potential = second + offset;
            if (!grid.IsOutOfBounds(potential))
            {
                antinodes.Add(potential);
            }
            return antinodes;
        }

        private static List<Coords> CalculateAntinodesPart2(char[][] grid, Coords first, Coords second)
        {
            var antinodes = new List<Coords>() { first, second };
            var offset = second - first;
            var potential = first;
            while (!grid.IsOutOfBounds(potential -= offset))
            {
                antinodes.Add(potential);
            }
            potential = second;
            while (!grid.IsOutOfBounds(potential += offset))
            {
                antinodes.Add(potential);
            }
            return antinodes;
        }

        private static Dictionary<char, List<Coords>> ToGroupedAntennas(char[][] grid)
        {
            var groupedAntennas = new Dictionary<char, List<Coords>>();
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == '.')
                    {
                        continue;
                    }
                    if (!groupedAntennas.TryGetValue(grid[y][x], out var antennas))
                    {
                        antennas = [];
                        groupedAntennas.Add(grid[y][x], antennas);
                    }
                    antennas.Add(new(x, y));
                }
            }
            return groupedAntennas;
        }
    }
}
