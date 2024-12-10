namespace AdventOfCode.Y2016.Tests
{
    public class Test14
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(22728, result);
        }

        [Fact(Skip = "Test takes too long (MD5)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped")]
        public void TestPart2()
        {
            var subject = new Day14();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(22551, result);
        }

        private const string _input = @"abc";
    }
}
