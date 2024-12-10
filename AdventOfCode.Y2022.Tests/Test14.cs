namespace AdventOfCode.Y2022.Tests
{
    public class Test14
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(24, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(93, result);
        }

        private const string _input = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9
";
    }
}
