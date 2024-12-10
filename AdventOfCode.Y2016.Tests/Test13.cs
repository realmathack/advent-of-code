namespace AdventOfCode.Y2016.Tests
{
    public class Test13
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day13(new(7, 4));
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(11, result);
        }

        private const string _input = @"10";
    }
}
