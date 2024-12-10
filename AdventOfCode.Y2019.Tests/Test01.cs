namespace AdventOfCode.Y2019.Tests
{
    public class Test01
    {
        [Theory]
        [InlineData("12", 2)]
        [InlineData("14", 2)]
        [InlineData("1969", 654)]
        [InlineData("100756", 33583)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day01();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("14", 2)]
        [InlineData("1969", 966)]
        [InlineData("100756", 50346)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day01();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
