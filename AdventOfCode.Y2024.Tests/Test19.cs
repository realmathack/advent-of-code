namespace AdventOfCode.Y2024.Tests
{
    public class Test19
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day19();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(6, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day19();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(16L, result);
        }

        private const string _input = @"r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb
";
    }
}
