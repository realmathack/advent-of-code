namespace AdventOfCode.Y2018.Tests
{
    public class Test11
    {
        [Theory]
        [InlineData("18", "33,45")]
        [InlineData("42", "21,61")]
        public void TestPart1(string input, string expected)
        {
            var subject = new Day11();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("18", "90,269,16")]
        [InlineData("42", "232,251,12")]
        public void TestPart2(string input, string expected)
        {
            var subject = new Day11();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
