namespace AdventOfCode.Y2015.Tests
{
    public class Test04
    {
        [Theory]
        [InlineData("abcdef", 609043)]
        [InlineData("pqrstuv", 1048970)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day04();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }
    }
}
