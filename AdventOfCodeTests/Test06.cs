namespace AdventOfCodeTests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(7, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(19, result);
        }

        private const string _input = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";
    }
}
