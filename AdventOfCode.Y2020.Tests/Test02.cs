namespace AdventOfCode.Y2020.Tests
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

            Assert.Equal(1, result);
        }

        private const string _input = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc
";
    }
}
