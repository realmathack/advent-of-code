namespace AdventOfCode.Y2017.Tests
{
    public class Test12
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(6, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(2, result);
        }

        private const string _input = @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5
";
    }
}
