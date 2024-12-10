namespace AdventOfCode.Y2015.Tests
{
    public class Test02
    {
        [Theory]
        [InlineData("2x3x4", 58)]
        [InlineData("1x1x10", 43)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day02();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2x3x4", 34)]
        [InlineData("1x1x10", 14)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day02();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
