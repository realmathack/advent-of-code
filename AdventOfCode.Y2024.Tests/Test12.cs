namespace AdventOfCode.Y2024.Tests
{
    public class Test12
    {
        [Theory]
        [InlineData(_input1, 140)]
        [InlineData(_input2, 772)]
        [InlineData(_input, 1930)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day12();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(_input1, 80)]
        [InlineData(_input2, 436)]
        [InlineData(_input3, 236)]
        [InlineData(_input4, 368)]
        [InlineData(_input, 1206)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day12();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }

        private const string _input = @"RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE
";
        private const string _input1 = @"AAAA
BBCD
BBCC
EEEC
";
        private const string _input2 = @"OOOOO
OXOXO
OOOOO
OXOXO
OOOOO
";
        private const string _input3 = @"EEEEE
EXXXX
EEEEE
EXXXX
EEEEE
";
        private const string _input4 = @"AAAAAA
AAABBA
AAABBA
ABBAAA
ABBAAA
AAAAAA
";
    }
}
