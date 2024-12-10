namespace AdventOfCode.Y2023.Tests
{
    public class Test22
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day22();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(5, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day22();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(7, result);
        }

        private const string _input = @"1,0,1~1,2,1
0,0,2~2,0,2
0,2,3~2,2,3
0,0,4~0,2,4
2,0,5~2,2,5
0,1,6~2,1,6
1,1,8~1,1,9
";
    }
}
