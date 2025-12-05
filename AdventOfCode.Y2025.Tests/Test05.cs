namespace AdventOfCode.Y2025.Tests
{
    public class Test05
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(3, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(14L, result);
        }

        private const string _input = @"3-5
10-14
16-20
12-18

1
5
8
11
17
32
";
    }
}
