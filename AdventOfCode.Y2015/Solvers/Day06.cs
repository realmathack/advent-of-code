using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public partial class Day06 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var grid = InitBoolGrid();
            foreach (var move in ToMoves(input))
            {
                for (int y = move.TopX; y <= move.BottomX; y++)
                {
                    for (int x = move.TopY; x <= move.BottomY; x++)
                    {
                        grid[y][x] = move.Change switch
                        {
                            Change.Off => false,
                            Change.On => true,
                            Change.Toggle => !grid[y][x],
                            _ => throw new InvalidOperationException($"Unknown change: {move.Change}")
                        };
                    }
                }
            }
            return grid.Select(row => row.Count(light => light)).Sum();
        }

        public override object SolvePart2(string[] input)
        {
            var grid = InitInt32Grid();
            foreach (var move in ToMoves(input))
            {
                for (int y = move.TopX; y <= move.BottomX; y++)
                {
                    for (int x = move.TopY; x <= move.BottomY; x++)
                    {
                        grid[y][x] = move.Change switch
                        {
                            Change.Off => Math.Max(0, grid[y][x] - 1),
                            Change.On => grid[y][x] + 1,
                            Change.Toggle => grid[y][x] + 2,
                            _ => throw new InvalidOperationException($"Unknown change: {move.Change}")
                        };
                    }
                }
            }
            return grid.Sum(row => row.Sum());
        }

        private static List<Move> ToMoves(string[] lines)
        {
            var moves = new List<Move>(lines.Length);
            foreach (var line in lines)
            {
                var match = MoveRegex().Match(line);
                var change = match.Groups[1].Value switch
                {
                    "turn off" => Change.Off,
                    "turn on" => Change.On,
                    "toggle" => Change.Toggle,
                    _ => throw new InvalidOperationException($"Unknown change: {match.Groups[1].Value}")
                };
                moves.Add(new(change, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value)));
            }
            return moves;
        }

        private static bool[][] InitBoolGrid()
        {
            var grid = new bool[1000][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new bool[1000];
            }
            return grid;
        }

        private static int[][] InitInt32Grid()
        {
            var grid = new int[1000][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new int[1000];
            }
            return grid;
        }

        [GeneratedRegex(@"(.+) (\d+),(\d+) through (\d+),(\d+)")]
        private static partial Regex MoveRegex();

        private enum Change { Off, On, Toggle }
        private record class Move(Change Change, int TopX, int TopY, int BottomX, int BottomY);
    }
}
