namespace AdventOfCode.Y2023.Tests
{
    public class Test11
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(374L, result);
        }

        [Theory]
        [InlineData(10, 1030L)]
        [InlineData(100, 8410L)]
        public void TestPart2(int timesLarger, long expected)
        {
            var subject = new Day11(timesLarger);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }

        private const string _input = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....
";
    }
}
