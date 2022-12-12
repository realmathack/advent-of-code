namespace AdventOfCode.Y2015.Tests
{
    public class Test24
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day24();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(99L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day24();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(44L, result);
        }

        private const string _input = @"1
2
3
4
5
7
8
9
10
11
";
    }
}
