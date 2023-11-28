namespace AdventOfCode.Y2015.Tests
{
    public class Test10
    {
        [Theory]
        [InlineData("1", 1, 2)]
        [InlineData("1", 2, 2)]
        [InlineData("1", 3, 4)]
        [InlineData("1", 4, 6)]
        [InlineData("1", 5, 6)]
        public void TestLookAndSay(string input, int times, int expected)
        {
            var result = Day10.LookAndSay(input, times);

            Assert.Equal(expected, result);
        }
    }
}
