namespace AdventOfCode.Y2016.Tests
{
    public class Test13
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(11, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(null!, result);
        }

        private const string _input = @"10
7,4
";
    }
}
