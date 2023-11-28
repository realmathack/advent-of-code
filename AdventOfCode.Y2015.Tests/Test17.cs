namespace AdventOfCode.Y2015.Tests
{
    public class Test17
    {
        [Fact]
        public void TestPart1()
        {
            var result = Day17.SolvePart1(_input.SplitIntoLines(), 25);

            Assert.Equal(4, result);
        }

        [Fact]
        public void TestPart2()
        {
            var result = Day17.SolvePart2(_input.SplitIntoLines(), 25);

            Assert.Equal(3, result);
        }

        private const string _input = @"20
15
10
5
5
";
    }
}
