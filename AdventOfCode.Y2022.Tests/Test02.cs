namespace AdventOfCode.Y2022.Tests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(15, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(12, result);
        }

        private const string _input = @"A Y
B X
C Z
";
    }
}
