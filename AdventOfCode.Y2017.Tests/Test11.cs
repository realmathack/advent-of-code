namespace AdventOfCode.Y2017.Tests
{
    public class Test11
    {
        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 0)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day11();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }
    }
}
