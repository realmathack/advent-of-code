namespace AdventOfCode.Y2022.Tests
{
    public class Test20
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day20();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(3L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day20();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(1623178306L, result);
        }

        private const string _input = @"1
2
-3
3
-2
0
4
";
    }
}
