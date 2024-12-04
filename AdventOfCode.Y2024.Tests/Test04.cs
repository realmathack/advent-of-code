namespace AdventOfCode.Y2024.Tests
{
    public class Test04
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(18, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(9, result);
        }

        private const string _input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
";
    }
}
