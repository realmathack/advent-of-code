namespace AdventOfCode.Y2018.Tests
{
    public class Test08
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(138, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(66, result);
        }

        private const string _input = @"2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
    }
}
