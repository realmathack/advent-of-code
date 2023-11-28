namespace AdventOfCode.Y2018.Tests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();
            subject.SetInput(_input1);

            var result = subject.SolvePart1();

            Assert.Equal(12, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();
            subject.SetInput(_input2);

            var result = subject.SolvePart2();

            Assert.Equal("fgij", result);
        }

        private const string _input1 = @"abcdef
bababc
abbcde
abcccd
aabcdd
abcdee
ababab
";
        private const string _input2 = @"abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz
";
    }
}
