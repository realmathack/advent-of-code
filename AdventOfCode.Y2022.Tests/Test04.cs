namespace AdventOfCode.Y2022.Tests
{
    public class Test04
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(2, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(4, result);
        }

        private const string _input = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8
";
    }
}
