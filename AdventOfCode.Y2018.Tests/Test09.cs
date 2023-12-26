namespace AdventOfCode.Y2018.Tests
{
    public class Test09
    {
        [Theory]
        [InlineData("9 players; last marble is worth 25 points", 32L)]
        [InlineData("10 players; last marble is worth 1618 points", 8317L)]
        [InlineData("13 players; last marble is worth 7999 points", 146373L)]
        [InlineData("17 players; last marble is worth 1104 points", 2764L)]
        [InlineData("21 players; last marble is worth 6111 points", 54718L)]
        [InlineData("30 players; last marble is worth 5807 points", 37305L)]
        public void TestPart1(string input, long expected)
        {
            var subject = new Day09();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }
    }
}
