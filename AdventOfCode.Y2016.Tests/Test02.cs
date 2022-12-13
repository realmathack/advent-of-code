namespace AdventOfCode.Y2016.Tests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal("1985", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal("5DB3", result);
        }

        private const string _input = @"ULL
RRDDD
LURDL
UUUUD
";
    }
}
