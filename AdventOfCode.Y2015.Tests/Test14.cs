namespace AdventOfCode.Y2015.Tests
{
    public class Test14
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(2660, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(1564, result);
        }

        private const string _input = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dasher can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
";
    }
}
