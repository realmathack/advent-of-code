namespace AdventOfCode.Y2021.Tests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(5934L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(26984457539L, result);
        }

        private const string _input = @"3,4,3,1,2";
    }
}
