namespace AdventOfCode.Y2022.Tests
{
    public class Test12
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(31, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(29, result);
        }

        private const string _input = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi
";
    }
}
