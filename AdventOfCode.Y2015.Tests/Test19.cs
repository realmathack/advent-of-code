namespace AdventOfCode.Y2015.Tests
{
    public class Test19
    {
        [Theory]
        [InlineData($"{_replacementsPart1}HOH", 4)]
        [InlineData($"{_replacementsPart1}HOHOHO", 7)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day19();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData($"{_replacementsPart2}HOH", 3)]
        [InlineData($"{_replacementsPart2}HOHOHO", 6)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day19();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }

        private const string _replacementsPart1 = @"H => HO
H => OH
O => HH

";
        private const string _replacementsPart2 = @"H => HO
H => OH
O => HH
e => H
e => O

";
    }
}
