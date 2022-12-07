namespace AdventOfCode.Y2022.Tests
{
    public class Test00
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day00();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(null, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day00();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(null, result);
        }

        private const string _input = @"";
    }
}
