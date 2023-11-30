namespace AdventOfCode.Y2020.Tests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(7, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(336, result);
        }

        private const string _input = @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#
";
    }
}
