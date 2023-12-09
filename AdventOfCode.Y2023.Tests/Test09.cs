namespace AdventOfCode.Y2023.Tests
{
    public class Test09
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(114, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(2, result);
        }

        private const string _input = @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45
";
    }
}
