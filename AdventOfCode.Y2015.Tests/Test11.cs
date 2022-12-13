namespace AdventOfCode.Y2015.Tests
{
    public class Test11
    {
        [Theory]
        [InlineData("abcdefgh", "abcdffaa")]
        [InlineData("ghijklmn", "ghjaabcc")]
        public void TestPart1(string input, string expected)
        {
            var subject = new Day11();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }
    }
}
