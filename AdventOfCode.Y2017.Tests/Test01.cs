namespace AdventOfCode.Y2017.Tests
{
    public class Test01
    {
        [Theory]
        [InlineData("1122", 3)]
        [InlineData("1111", 4)]
        [InlineData("1234", 0)]
        [InlineData("91212129", 9)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day01();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1212", 6)]
        [InlineData("1221", 0)]
        [InlineData("123425", 4)]
        [InlineData("123123", 12)]
        [InlineData("12131415", 4)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day01();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
