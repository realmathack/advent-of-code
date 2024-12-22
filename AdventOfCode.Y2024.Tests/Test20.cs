namespace AdventOfCode.Y2024.Tests
{
    public class Test20
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day20(20);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(5, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day20(50);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(285, result);
        }

        private const string _input = @"###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
";
    }
}
