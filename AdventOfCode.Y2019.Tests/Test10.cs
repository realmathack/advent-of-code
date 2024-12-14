namespace AdventOfCode.Y2019.Tests
{
    public class Test10
    {
        [Theory]
        [InlineData(_input1, 8)]
        [InlineData(_input2, 33)]
        [InlineData(_input3, 35)]
        [InlineData(_input4, 41)]
        [InlineData(_input5, 210)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day10();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day10();
            subject.SetInput(_input5);

            var result = subject.SolvePart2();

            Assert.Equal(802, result);
        }

        private const string _input1 = @".#..#
.....
#####
....#
...##
";
        private const string _input2 = @"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####
";
        private const string _input3 = @"#.#...#.#.
.###....#.
.#....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.
";
        private const string _input4 = @".#..#..###
####.###.#
....###.#.
..###.##.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..
";
        private const string _input5 = @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##
";
    }
}
