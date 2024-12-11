namespace AdventOfCode.Y2024.Tests
{
    public class Test11
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(55312L, result);
        }

        private const string _input = @"125 17";
    }
}
