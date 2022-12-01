namespace AdventOfCodeTests
{
    public class Test01
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day01();

            var result = subject.SolvePart1(_input);

            Assert.Equal("24000", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day01();

            var result = subject.SolvePart2(_input);

            Assert.Equal("45000", result);
        }

        private const string _input = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";
    }
}