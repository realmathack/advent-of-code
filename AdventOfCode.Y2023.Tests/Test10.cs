namespace AdventOfCode.Y2023.Tests
{
    public class Test10
    {
        [Theory]
        [InlineData(_input1, 4)]
        [InlineData(_input2, 8)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day10();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(_input1, 1)]
        [InlineData(_input2, 1)]
        [InlineData(_input3, 4)]
        [InlineData(_input4, 4)]
        [InlineData(_input5, 8)]
        [InlineData(_input6, 10)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day10();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }

        private const string _input1 = @"-L|F7
7S-7|
L|7||
-L-J|
L|-JF
";
        private const string _input2 = @"7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ
";
        private const string _input3 = @"...........
.S-------7.
.|F-----7|.
.||.....||.
.||.....||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........
";
        private const string _input4 = @"..........
.S------7.
.|F----7|.
.||....||.
.||....||.
.|L-7F-J|.
.|..||..|.
.L--JL--J.
..........
";
        private const string _input5 = @".F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...
";
        private const string _input6 = @"FF7FSF7F7F7F7F7F---7
L|LJ||||||||||||F--J
FL-7LJLJ||||||LJL-77
F--JF--7||LJLJ7F7FJ-
L---JF-JLJ.||-FJLJJ7
|F|F-JF---7F7-L7L|7|
|FFJF7L7F-JF7|JL---7
7-L-JL7||F7|L7F-7F7|
L.L7LFJ|||||FJL7||LJ
L7JLJL-JLJLJL--JLJ.L
";
    }
}
