namespace AdventOfCode.Y2023.Tests
{
    public class Test07
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(6440L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(5905L, result);
        }

        private const string _input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483
";
    }
}
