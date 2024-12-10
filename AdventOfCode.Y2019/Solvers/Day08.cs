namespace AdventOfCode.Y2019.Solvers
{
    public class Day08(int _width, int _height, bool _readScreen = false) : SolverWithText
    {
        public Day08() : this(25, 6, true) { }

        public override object SolvePart1(string input)
        {
            var lowest = int.MaxValue;
            var score = 0;
            var size = _width * _height;
            var layers = input.Length / size;
            for (int layer = 0; layer < layers; layer++)
            {
                var counts = input[(layer * size)..((layer + 1) * size)].GroupBy(number => number).Select(g => (g.Key, Count: g.Count())).ToDictionary();
                if (counts.TryGetValue('0', out var count) && count < lowest)
                {
                    lowest = count;
                    counts.TryGetValue('1', out var ones);
                    counts.TryGetValue('2', out var twos);
                    score = ones * twos;
                }
            }
            return score;
        }

        public override object SolvePart2(string input)
        {
            var size = _width * _height;
            var layers = input.Length / size;
            var screen = new Screen(_width, _height);
            for (int i = 0; i < size; i++)
            {
                for (int layer = 0; layer < layers; layer++)
                {
                    var number = input[i + layer * size];
                    if (number == '2')
                    {
                        continue;
                    }
                    if (number == '1')
                    {
                        screen.SetPixel(i % _width, i / _width);
                    }
                    break;
                }
            }
            return _readScreen ? screen.ReadScreen() : screen.PrintScreen();
        }
    }
}
