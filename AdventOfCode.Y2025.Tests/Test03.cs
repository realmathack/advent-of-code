namespace AdventOfCode.Y2025.Tests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(357L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(3121910778619L, result);
        }

        private const string _input = @"987654321111111
811111111111119
234234234234278
818181911112111
";
    }
}
