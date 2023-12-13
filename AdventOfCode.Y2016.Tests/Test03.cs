namespace AdventOfCode.Y2016.Tests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(0, result);
        }

        private const string _input = @"5 10 25";
    }
}
