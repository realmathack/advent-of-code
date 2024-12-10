namespace AdventOfCode.Y2023.Tests
{
    public class Test18
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day18();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(62L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day18();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(952408144115L, result);
        }

        private const string _input = @"R 6 (#70c710)
D 5 (#0dc571)
L 2 (#5713f0)
D 2 (#d2c081)
R 2 (#59c680)
D 2 (#411b91)
L 5 (#8ceee2)
U 2 (#caa173)
L 1 (#1b58a2)
U 2 (#caa171)
R 2 (#7807d2)
U 3 (#a77fa3)
L 2 (#015232)
U 2 (#7a21e3)
";
    }
}
