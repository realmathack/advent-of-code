namespace AdventOfCode.Y2023.Tests
{
    public class Test14
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(136, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(64, result);
        }

        private const string _input = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....
";
    }
}
