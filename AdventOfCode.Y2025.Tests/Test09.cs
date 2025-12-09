namespace AdventOfCode.Y2025.Tests
{
    public class Test09
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(50L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(24L, result);
        }

        private const string _input = @"7,1
11,1
11,7
9,7
9,5
2,5
2,3
7,3
";
    }
}
