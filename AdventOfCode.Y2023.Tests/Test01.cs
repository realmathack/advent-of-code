namespace AdventOfCode.Y2023.Tests
{
    public class Test01
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day01();
            subject.SetInput(_input1);

            var result = subject.SolvePart1();

            Assert.Equal(142, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day01();
            subject.SetInput(_input2);

            var result = subject.SolvePart2();

            Assert.Equal(281, result);
        }

        private const string _input1 = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet
";

        private const string _input2 = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen
";
    }
}
