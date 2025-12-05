using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2025.Solvers
{
    public class Day04 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] input)
        {
            var total = 0;
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '@' && new Coords(x, y).Adjacents.Count(coord => IsPaperRoll(input, coord)) < 4)
                    {
                        total++;
                    }
                }
            }
            return total;
        }

        public override object SolvePart2(char[][] input)
        {
            var total = 0;
            var startTotal = 0;
            do
            {
                startTotal = total;
                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[y].Length; x++)
                    {
                        if (input[y][x] == '@' && new Coords(x, y).Adjacents.Count(coord => IsPaperRoll(input, coord)) < 4)
                        {
                            input[y][x] = '.';
                            total++;
                        }
                    }
                }
            } while (startTotal != total);
            return total;
        }

        private static bool IsPaperRoll(char[][] input, Coords coord)
        {
            if (coord.Y < 0 || coord.X < 0 || coord.Y >= input.Length || coord.X >= input[0].Length)
            {
                return false;
            }
            return input[coord.Y][coord.X] == '@';
        }
    }
}
