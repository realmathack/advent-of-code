namespace AdventOfCode.Y2023.Tests
{
    public class Test13
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(405, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(400, result);
        }

        private const string _input = @"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#
";
    }
}
