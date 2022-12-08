namespace AdventOfCode.Y2015.Tests
{
    public class Test03
    {
        [Theory]
        [InlineData(">", 2)]
        [InlineData("^>v<", 4)]
        [InlineData("^v^v^v^v^v", 2)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day03();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("^v", 3)]
        [InlineData("^>v<", 3)]
        [InlineData("^v^v^v^v^v", 11)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day03();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
