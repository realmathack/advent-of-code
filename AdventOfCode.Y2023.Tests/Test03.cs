namespace AdventOfCode.Y2023.Tests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(4361, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(467835, result);
        }

        private const string _input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..
";
    }
}
