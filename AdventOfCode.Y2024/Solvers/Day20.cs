using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day20(int timeLimit) : SolverWithCharGrid
    {
        public Day20() : this(100) { }

        public override object SolvePart1(char[][] grid)
        {
            var (start, end) = GetStartAndEnd(grid);
            return CalculateCheats(grid, start, end).Count(cheat => cheat.Value >= timeLimit);
        }

        public override object SolvePart2(char[][] grid)
        {
            var (start, end) = GetStartAndEnd(grid);
            // TODO: Implement
            return null!;
        }

        private static Dictionary<(Coords Start, Coords End), int> CalculateCheats(char[][] grid, Coords start, Coords end)
        {
            var distances = GetPathDistances(grid, start, end);
            var cheats = new Dictionary<(Coords Start, Coords End), int>();
            foreach (var node in distances)
            {
                foreach (var offset in Coords.NeighborOffsets)
                {
                    var potential = node.Key + offset;
                    if (grid[potential.Y][potential.X] != '#')
                    {
                        continue;
                    }
                    potential += offset;
                    if (grid.IsOutOfBounds(potential) || grid[potential.Y][potential.X] != '.')
                    {
                        continue;
                    }
                    if (!distances.TryGetValue(potential, out var tmp))
                    {
                        continue;
                    }
                    var timeSaved = node.Value - tmp - 2;
                    if (timeSaved > 0)
                    {
                        cheats.Add((node.Key, potential), timeSaved);
                    }
                }
            }
            return cheats;
        }

        private static Dictionary<Coords, int> GetPathDistances(char[][] grid, Coords start, Coords end)
        {
            var distances = new Dictionary<Coords, int>() { [end] = 0 };
            var current = end;
            var distance = 1;
            var last = end;
            while (current != start)
            {
                var next = current;
                foreach (var potential in current.Neighbors)
                {
                    if (grid[potential.Y][potential.X] == '.' && potential != last)
                    {
                        next = potential;
                    }
                }
                last = current;
                current = next;
                distances.Add(current, distance++);
            }
            return distances;
        }

        private static (Coords Start, Coords End) GetStartAndEnd(char[][] grid)
        {
            Coords? start = null;
            Coords? end = null;
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (start is null && grid[y][x] == 'S')
                    {
                        start = new Coords(x, y);
                        grid[y][x] = '.';
                    }
                    if (end is null && grid[y][x] == 'E')
                    {
                        end = new Coords(x, y);
                        grid[y][x] = '.';
                    }
                }
            }
            if (start is null || end is null)
            {
                throw new ImpossibleException("Start and/or End not found!");
            }
            return (start.Value, end.Value);
        }
    }
}
