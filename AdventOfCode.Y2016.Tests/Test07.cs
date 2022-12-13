namespace AdventOfCode.Y2016.Tests
{
    public class Test07
    {
        [Theory]
        [InlineData("abba[mnop]qrst", 1)]
        [InlineData("abcd[bddb]xyyx", 0)]
        [InlineData("aaaa[qwer]tyui", 0)]
        [InlineData("ioxxoj[asdfgh]zxcvbn", 1)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day07();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("aba[bab]xyz", 1)]
        [InlineData("xyx[xyx]xyx", 0)]
        [InlineData("aaa[kek]eke", 1)]
        [InlineData("zazbz[bzb]cdb", 1)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day07();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
