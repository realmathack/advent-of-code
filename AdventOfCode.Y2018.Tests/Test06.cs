namespace AdventOfCode.Y2018.Tests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(17, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06(32);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(16, result);
        }

        private const string _input = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9
";
    }
}
