namespace AdventOfCode.Y2023.Tests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(288L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(71503L, result);
        }

        private const string _input = @"Time:      7  15   30
Distance:  9  40  200
";
    }
}
