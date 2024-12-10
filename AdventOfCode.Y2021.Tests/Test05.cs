namespace AdventOfCode.Y2021.Tests
{
    public class Test05
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(5, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(12, result);
        }

        private const string _input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2
";
    }
}
