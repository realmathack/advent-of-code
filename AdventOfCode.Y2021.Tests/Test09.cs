namespace AdventOfCode.Y2021.Tests
{
    public class Test09
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(15, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(1134, result);
        }

        private const string _input = @"2199943210
3987894921
9856789892
8767896789
9899965678
";
    }
}
