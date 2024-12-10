namespace AdventOfCode.Y2015.Tests
{
    public class Test20
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day20();
            subject.SetInput("150");

            var result = subject.SolvePart1();

            Assert.Equal(8, result);
        }
    }
}
