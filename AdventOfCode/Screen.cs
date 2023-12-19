namespace AdventOfCode
{
    public class Screen
    {
        private readonly bool[][] _grid;

        public Screen(bool[][] grid) => _grid = grid;

        public Screen(int width, int height)
        {
            _grid = new bool[height][];
            for (int i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new bool[width];
            }
        }

        public void DrawPixel(int x, int y, bool value = true) => _grid[y][x] = value;
        public string PrintScreen() => string.Concat(_grid.Select(row => Environment.NewLine + string.Concat(row.Select(value => value ? '#' : '.'))));

        public string ReadScreen()
        {
            if (_grid.Length != 6 || _grid[0].Length % 5 != 0)
            {
                return PrintScreen();
            }
            var pixels = new List<int[]>();
            for (int i = 0; i < _grid[0].Length; i += 5)
            {
                pixels.Add(_grid.SelectMany(row => row[i..(i + 5)].Select(value => value ? 1 : 0)).ToArray());
            }
            return string.Concat(pixels.Select(GetLetter));
        }

        private static char GetLetter(int[] pixels)
        {
            if (pixels is [0, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 1, 1, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0]) return 'A';
            if (pixels is [1, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 1, 1, 0, 0]) return 'B';
            if (pixels is [0, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 1, 0,  0, 1, 1, 0, 0]) return 'C';
            if (pixels is [1, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 1, 1, 0, 0]) return 'D';
            if (pixels is [1, 1, 1, 1, 0,  1, 0, 0, 0, 0,  1, 1, 1, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  1, 1, 1, 1, 0]) return 'E';
            if (pixels is [1, 1, 1, 1, 0,  1, 0, 0, 0, 0,  1, 1, 1, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0]) return 'F';
            if (pixels is [0, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 0, 0,  1, 0, 1, 1, 0,  1, 0, 0, 1, 0,  0, 1, 1, 1, 0]) return 'G';
            if (pixels is [1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 1, 1, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0]) return 'H';
            if (pixels is [0, 1, 1, 1, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 1, 1, 1, 0]) return 'I';
            if (pixels is [0, 0, 1, 1, 0,  0, 0, 0, 1, 0,  0, 0, 0, 1, 0,  0, 0, 0, 1, 0,  1, 0, 0, 1, 0,  0, 1, 1, 0, 0]) return 'J';
            if (pixels is [1, 0, 0, 1, 0,  1, 0, 1, 0, 0,  1, 1, 0, 0, 0,  1, 0, 1, 0, 0,  1, 0, 1, 0, 0,  1, 0, 0, 1, 0]) return 'K';
            if (pixels is [1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  1, 1, 1, 1, 0]) return 'L';
            if (pixels is [1, 0, 0, 0, 1,  1, 1, 0, 1, 1,  1, 0, 1, 0, 1,  1, 0, 0, 0, 1,  1, 0, 0, 0, 1,  1, 0, 0, 0, 1]) return 'M';
            if (pixels is [1, 0, 0, 1, 0,  1, 1, 0, 1, 0,  1, 1, 0, 1, 0,  1, 0, 1, 1, 0,  1, 0, 1, 1, 0,  1, 0, 0, 1, 0]) return 'N';
            if (pixels is [0, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  0, 1, 1, 0, 0]) return 'O';
            if (pixels is [1, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 1, 1, 0, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0]) return 'P';
            if (pixels is [0, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  0, 1, 1, 1, 1]) return 'Q';
            if (pixels is [1, 1, 1, 0, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 1, 1, 0, 0,  1, 0, 1, 0, 0,  1, 0, 0, 1, 0]) return 'R';
            if (pixels is [0, 1, 1, 1, 0,  1, 0, 0, 0, 0,  1, 0, 0, 0, 0,  0, 1, 1, 0, 0,  0, 0, 0, 1, 0,  1, 1, 1, 0, 0]) return 'S';
            if (pixels is [1, 1, 1, 1, 1,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0]) return 'T';
            if (pixels is [1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  1, 0, 0, 1, 0,  0, 1, 1, 0, 0]) return 'U';
            if (pixels is [1, 0, 0, 0, 1,  1, 0, 0, 0, 1,  0, 1, 0, 1, 0,  0, 1, 0, 1, 0,  0, 1, 0, 1, 0,  0, 0, 1, 0, 0]) return 'V';
            if (pixels is [1, 0, 0, 0, 1,  1, 0, 0, 0, 1,  1, 0, 0, 0, 1,  1, 0, 1, 0, 1,  1, 1, 0, 1, 1,  1, 0, 1, 0, 1]) return 'W';
            if (pixels is [1, 0, 0, 0, 1,  0, 1, 0, 1, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 1, 0, 1, 0,  1, 0, 0, 0, 1]) return 'X';
            if (pixels is [1, 0, 0, 0, 1,  1, 0, 0, 0, 1,  0, 1, 0, 1, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 0,  0, 0, 1, 0, 1]) return 'Y';
            if (pixels is [1, 1, 1, 1, 0,  0, 0, 0, 1, 0,  0, 0, 1, 0, 0,  0, 1, 0, 0, 0,  1, 0, 0, 0, 0,  1, 1, 1, 1, 0]) return 'Z';
            return ' ';
        }
    }
}
