namespace AdventOfCode.Y2023.Tests
{
    public class Test17
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day17();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(102, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day17();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(94, result);
        }

        private const string _input = @"2413432311323
3215453535623
3255245654254
3446585845452
4546657867536
1438598798454
4457876987766
3637877979653
4654967986887
4564679986453
1224686865563
2546548887735
4322674655533
";
    }
}
