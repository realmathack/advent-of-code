namespace AdventOfCode.Y2016.Tests
{
    public class Test01
    {
        [Theory]
        [InlineData("R2, L3", 5)]
        [InlineData("R2, R2, R2", 2)]
        [InlineData("R5, L5, R5, R3", 12)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day01();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day01();
            subject.SetInput("R8, R4, R4, R8");

            var result = subject.SolvePart2();

            Assert.Equal(4, result);
        }
    }
}
