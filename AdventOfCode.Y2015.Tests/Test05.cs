namespace AdventOfCode.Y2015.Tests
{
    public class Test05
    {
        [Theory]
        [InlineData("ugknbfddgicrmopn", 1)]
        [InlineData("aaa", 1)]
        [InlineData("jchzalrnumimnmhp", 0)]
        [InlineData("haegwjzuvuyypxyu", 0)]
        [InlineData("dvszwmarrgswjxmb", 0)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day05();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("qjhvhtzxzqqjkmpb", 1)]
        [InlineData("xxyxx", 1)]
        [InlineData("uurcxstgmygtbstg", 0)]
        [InlineData("ieodomkazucvgmuy", 0)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day05();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
