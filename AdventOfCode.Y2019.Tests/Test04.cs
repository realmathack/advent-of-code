namespace AdventOfCode.Y2019.Tests
{
    public class Test04
    {
        [Theory]
        [InlineData(111111, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void TestPart1(int input, bool expected)
        {
            var result = Day04.IsPassword1(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        public void TestPart2(int input, bool expected)
        {
            var result = Day04.IsPassword2(input);

            Assert.Equal(expected, result);
        }
    }
}
