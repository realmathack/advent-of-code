namespace AdventOfCode.Y2024.Tests
{
    public class Test18
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day18(7, 12);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(22, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day18(7, 12);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal("6,1", result);
        }

        private const string _input = @"5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0
";
    }
}
