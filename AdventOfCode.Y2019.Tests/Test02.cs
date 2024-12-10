namespace AdventOfCode.Y2019.Tests
{
    public class Test02
    {
        [Theory]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", 3500)]
        [InlineData("1,0,0,0,99", 2)]
        [InlineData("2,3,0,3,99", 2)]
        [InlineData("2,4,4,5,99,0", 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 30)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day02(false);
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }
    }
}
