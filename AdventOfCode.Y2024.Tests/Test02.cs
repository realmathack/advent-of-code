namespace AdventOfCode.Y2024.Tests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(2, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(4, result);
        }

        private const string _input = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
";
    }
}
