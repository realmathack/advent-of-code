namespace AdventOfCode.Y2016.Tests
{
    public class Test05
    {
        [Fact(Skip = "Test takes too long")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped")]
        public void TestPart1()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal("18f47a30", result);
        }

        [Fact(Skip = "Test takes too long")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped")]
        public void TestPart2()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal("05ace8e3", result);
        }

        private const string _input = @"abc";
    }
}
