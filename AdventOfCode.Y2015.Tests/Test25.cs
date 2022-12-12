namespace AdventOfCode.Y2015.Tests
{
    public class Test25
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day25();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(1534922L, result);
        }

        private const string _input = @"To continue, please consult the code grid in the manual.  Enter the code at row 6, column 5.";
    }
}
