namespace AdventOfCode.Y2021.Tests
{
    public class Test11
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(1656, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(195, result);
        }

        private const string _input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526
";
    }
}
