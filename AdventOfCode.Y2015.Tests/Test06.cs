namespace AdventOfCode.Y2015.Tests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(998996, result);
        }

        [Theory]
        [InlineData("turn on 0,0 through 0,0", 1)]
        [InlineData("toggle 0,0 through 999,999", 2000000)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day06();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }

        private const string _input = @"turn on 0,0 through 999,999
toggle 0,0 through 999,0
turn off 499,499 through 500,500
";
    }
}
