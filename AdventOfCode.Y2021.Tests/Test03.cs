namespace AdventOfCode.Y2021.Tests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(198, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(230, result);
        }

        private const string _input = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010
";
    }
}
