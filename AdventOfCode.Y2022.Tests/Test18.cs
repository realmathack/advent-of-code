namespace AdventOfCode.Y2022.Tests
{
    public class Test18
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day18();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(64, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day18();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(58, result);
        }

        private const string _input = @"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5
";
    }
}
