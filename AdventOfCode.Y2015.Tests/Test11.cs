namespace AdventOfCode.Y2015.Tests
{
    public class Test11
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal("abcdffaa", result);
        }

        private const string _input = @"abcdefgh";
    }
}
