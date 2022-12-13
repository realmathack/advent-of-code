namespace AdventOfCode.Y2016.Tests
{
    public class Test08
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(6, result);
        }

        private const string _input = @"rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 1
";
    }
}
