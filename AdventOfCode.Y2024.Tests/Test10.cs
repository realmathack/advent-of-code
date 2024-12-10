namespace AdventOfCode.Y2024.Tests
{
    public class Test10
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day10();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(36, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day10();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(81, result);
        }

        private const string _input = @"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732
";
    }
}
