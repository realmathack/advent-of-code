namespace AdventOfCode.Y2015.Tests
{
    public class Test18
    {
        [Fact]
        public void TestPart1()
        {
            var result = Day18.SolvePart1(_input.SplitIntoLines(), 4);

            Assert.Equal(4, result);
        }

        [Fact]
        public void TestPart2()
        {
            var result = Day18.SolvePart2(_input.SplitIntoLines(), 5);

            Assert.Equal(17, result);
        }

        private const string _input = @".#.#.#
...##.
#....#
..#...
#.#..#
####..
";
    }
}
