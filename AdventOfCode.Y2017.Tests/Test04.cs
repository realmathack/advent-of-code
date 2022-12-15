namespace AdventOfCode.Y2017.Tests
{
    public class Test04
    {
        [Theory]
        [InlineData("aa bb cc dd ee", 1)]
        [InlineData("aa bb cc dd aa", 0)]
        [InlineData("aa bb cc dd aaa", 1)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day04();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abcde fghij", 1)]
        [InlineData("abcde xyz ecdab", 0)]
        [InlineData("a ab abc abd abf abj", 1)]
        [InlineData("iiii oiii ooii oooi oooo", 1)]
        [InlineData("oiii ioii iioi iiio", 0)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day04();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
