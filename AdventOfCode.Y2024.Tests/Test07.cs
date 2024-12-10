namespace AdventOfCode.Y2024.Tests
{
    public class Test07
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(3749L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(11387L, result);
        }

        private const string _input = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
";
    }
}
