namespace AdventOfCodeTests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();

            var result = subject.SolvePart1(_input);

            Assert.Equal("7", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06();

            var result = subject.SolvePart2(_input);

            Assert.Equal("19", result);
        }

        private const string _input = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";
    }
}
