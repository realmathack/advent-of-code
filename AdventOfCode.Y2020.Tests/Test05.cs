namespace AdventOfCode.Y2020.Tests
{
    public class Test05
    {
        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day05();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }
    }
}
