namespace AdventOfCode.Y2017.Tests
{
    public class Test10
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day10(5);
            subject.SetInput(@"3,4,1,5");

            var result = subject.SolvePart1();

            Assert.Equal(12, result);
        }

        [Theory]
        [InlineData("", "a2582a3a0e66e6e86e3812dcb672a272")]
        [InlineData("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd")]
        [InlineData("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d")]
        [InlineData("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e")]
        public void TestPart2(string input, string expected)
        {
            var subject = new Day10();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
