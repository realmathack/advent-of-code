using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2018.Solvers
{
    public class Day11 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var powerLevels = ToPowerLevels(input);
            var largest = 0L;
            var topLeft = new Coords(0, 0);
            for (int y = 1; y < powerLevels.Length - 2; y++)
            {
                for (int x = 1; x < powerLevels[y].Length - 2; x++)
                {
                    var power = Enumerable.Range(0, 3 * 3).Sum(i => powerLevels[y + i / 3][x + i % 3]);
                    if (power > largest)
                    {
                        largest = power;
                        topLeft = new(x, y);
                    }
                }
            }
            return topLeft.ToString();
        }

        public override object SolvePart2(string input)
        {
            var powerLevels = ToPowerLevels(input, true);
            var largest = 0L;
            var topLeft = new Coords(0, 0);
            var squareSize = 0;
            for (int size = 1; size <= 100; size++)
            {
                for (int y = size; y < powerLevels.Length; y++)
                {
                    for (int x = size; x < powerLevels[y].Length; x++)
                    {
                        var power = powerLevels[y][x] - powerLevels[y - size][x] - powerLevels[y][x - size] + powerLevels[y - size][x - size];
                        if (power > largest)
                        {
                            largest = power;
                            topLeft = new(x - size + 1, y - size + 1);
                            squareSize = size;
                        }
                    }
                }
            }
            return $"{topLeft},{squareSize}";
        }

        private static long[][] ToPowerLevels(string number, bool sumFromStart = false)
        {
            var serialNumber = long.Parse(number);
            var powerLevels = new long[301][];
            for (int i = 0; i < powerLevels.Length; i++)
            {
                powerLevels[i] = new long[301];
            }
            for (int y = 1; y < powerLevels.Length; y++)
            {
                for (int x = 1; x < powerLevels[y].Length; x++)
                {
                    var tmp = ((((x + 10) * y + serialNumber) * (x + 10)) % 1000) / 100 - 5;
                    if (sumFromStart)
                    {
                        tmp += powerLevels[y - 1][x] + powerLevels[y][x - 1] - powerLevels[y - 1][x - 1];
                    }
                    powerLevels[y][x] = tmp;
                }
            }
            return powerLevels;
        }
    }
}
