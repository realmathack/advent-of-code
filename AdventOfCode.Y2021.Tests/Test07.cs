namespace AdventOfCode.Y2021.Tests
{
    public class Test07
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(37, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(168, result);
        }

        private const string _input = @"16,1,2,0,4,2,7,1,2,14";
    }
}
