namespace AdventOfCode.Y2021.Tests
{
    public class Test01
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(7, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(5, result);
        }

        private const string _input = @"199
200
208
210
200
207
240
269
260
263
";
    }
}
