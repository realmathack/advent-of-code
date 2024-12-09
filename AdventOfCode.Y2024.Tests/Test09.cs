namespace AdventOfCode.Y2024.Tests
{
    public class Test09
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(1928L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(2858L, result);
        }

        private const string _input = @"2333133121414131402";
    }
}
