namespace AdventOfCodeTests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();

            var result = subject.SolvePart1(_input);

            Assert.Equal("15", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();

            var result = subject.SolvePart2(_input);

            Assert.Equal("12", result);
        }

        private const string _input = @"A Y
B X
C Z
";
    }
}
