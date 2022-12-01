namespace AdventOfCodeTests
{
    public class Test00
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day00();

            var result = subject.SolvePart1(_input);

            Assert.Equal("", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day00();

            var result = subject.SolvePart2(_input);

            Assert.Equal("", result);
        }

        private const string _input = @"";
    }
}
