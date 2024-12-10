namespace AdventOfCode.Y2015.Tests
{
    public class Test17
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day17(25);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(4, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day17(25);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(3, result);
        }

        private const string _input = @"20
15
10
5
5
";
    }
}
