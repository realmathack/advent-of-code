namespace AdventOfCode.Y2024.Tests
{
    public class Test01
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(11, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(31, result);
        }

        private const string _input = @"3   4
4   3
2   5
1   3
3   9
3   3
";
    }
}
