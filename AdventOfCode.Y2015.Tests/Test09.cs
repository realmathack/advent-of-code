namespace AdventOfCode.Y2015.Tests
{
    public class Test09
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(605, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(982, result);
        }

        private const string _input = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141
";
    }
}
