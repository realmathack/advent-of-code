
namespace AdventOfCode.Y2023.Solvers
{
    public class Day14 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var grid = input.ToCharGrid();
            MoveRoundedRocksNorth(grid);
            return CalculateLoad(grid);
        }

        public override object SolvePart2(string[] input)
        {
            var grid = input.ToCharGrid();
            var cache = new List<string>();
            var cycleStart = int.MinValue;
            for (int i = 0; i < 1_000_000_000; i++)
            {
                MoveRoundedRocksNorth(grid);
                MoveRoundedRocksWest(grid);
                MoveRoundedRocksSouth(grid);
                MoveRoundedRocksEast(grid);
                var tmp = string.Concat(grid.Select(row => string.Concat(row) + Environment.NewLine));
                if (cache.Contains(tmp))
                {
                    cycleStart = cache.IndexOf(tmp);
                    break;
                }
                cache.Add(tmp);
            }
            var index = cycleStart + ((1_000_000_000 - cycleStart) % (cache.Count - cycleStart)) - 1;
            grid = cache[index].SplitIntoLines().Select(line => line.ToCharArray()).ToArray();
            return CalculateLoad(grid);
        }

        private static void MoveRoundedRocksNorth(char[][] grid)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                var freeSpaces = new List<int>();
                for (int y = 0; y < grid.Length; y++)
                {
                    if (grid[y][x] == 'O')
                    {
                        if (freeSpaces.Count > 0)
                        {
                            grid[freeSpaces[0]][x] = 'O';
                            freeSpaces.RemoveAt(0);
                            grid[y][x] = '.';
                            freeSpaces.Add(y);
                        }
                    }
                    else if (grid[y][x] == '#')
                    {
                        freeSpaces.Clear();
                    }
                    else
                    {
                        freeSpaces.Add(y);
                    }
                }
            }
        }

        private static void MoveRoundedRocksWest(char[][] grid)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                var freeSpaces = new List<int>();
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == 'O')
                    {
                        if (freeSpaces.Count > 0)
                        {
                            grid[y][freeSpaces[0]] = 'O';
                            freeSpaces.RemoveAt(0);
                            grid[y][x] = '.';
                            freeSpaces.Add(x);
                        }
                    }
                    else if (grid[y][x] == '#')
                    {
                        freeSpaces.Clear();
                    }
                    else
                    {
                        freeSpaces.Add(x);
                    }
                }
            }
        }

        private static void MoveRoundedRocksSouth(char[][] grid)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                var freeSpaces = new List<int>();
                for (int y = grid.Length - 1; y >= 0; y--)
                {
                    if (grid[y][x] == 'O')
                    {
                        if (freeSpaces.Count > 0)
                        {
                            grid[freeSpaces[0]][x] = 'O';
                            freeSpaces.RemoveAt(0);
                            grid[y][x] = '.';
                            freeSpaces.Add(y);
                        }
                    }
                    else if (grid[y][x] == '#')
                    {
                        freeSpaces.Clear();
                    }
                    else
                    {
                        freeSpaces.Add(y);
                    }
                }
            }
        }

        private static void MoveRoundedRocksEast(char[][] grid)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                var freeSpaces = new List<int>();
                for (int x = grid[y].Length - 1; x >= 0; x--)
                {
                    if (grid[y][x] == 'O')
                    {
                        if (freeSpaces.Count > 0)
                        {
                            grid[y][freeSpaces[0]] = 'O';
                            freeSpaces.RemoveAt(0);
                            grid[y][x] = '.';
                            freeSpaces.Add(x);
                        }
                    }
                    else if (grid[y][x] == '#')
                    {
                        freeSpaces.Clear();
                    }
                    else
                    {
                        freeSpaces.Add(x);
                    }
                }
            }
        }

        private static int CalculateLoad(char[][] grid)
        {
            var sum = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                sum += grid[i].Count(space => space == 'O') * (grid.Length - i);
            }
            return sum;
        }
    }
}
