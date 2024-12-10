namespace AdventOfCode.Y2020.Tests
{
    public class Test01
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(514579, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(241861950L, result);
        }

        private const string _input = @"1721
979
366
299
675
1456
";
    }
}
