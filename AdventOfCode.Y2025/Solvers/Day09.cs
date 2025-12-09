using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2025.Solvers
{
    public class Day09 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var tiles = input.Select(Coords.Parse).ToArray();
            var largest = 0L;
            for (int a = 0; a < tiles.Length; a++)
            {
                for (int b = a + 1; b < tiles.Length; b++)
                {
                    var area = CalculateArea(tiles[a], tiles[b]);
                    if (area > largest)
                    {
                        largest = area;
                    }
                }
            }
            return largest;
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Implement (create minimized version of grid? -> https://github.com/maksverver/AdventOfCode/blob/master/2025/09.py)
            return null!;
        }

        private static long CalculateArea(Coords a, Coords b)
        {
            var height = 1L + Math.Abs(a.Y - b.Y);
            var width = 1L + Math.Abs(a.X - b.X);
            return height * width;
        }
    }
}
