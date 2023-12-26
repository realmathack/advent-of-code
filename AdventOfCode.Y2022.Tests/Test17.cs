namespace AdventOfCode.Y2022.Tests
{
    public class Test17
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day17();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(3068L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day17();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(1514285714288L, result);
        }

        private const string _input = @">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
    }
}
