namespace AdventOfCode.Y2015.Tests
{
    public class Test21
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day21();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(null, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day21();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(null, result);
        }

        private const string _input = @"";
    }
}
