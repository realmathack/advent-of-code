namespace AdventOfCode.Y2024.Tests
{
    public class Test08
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(14, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(34, result);
        }

        private const string _input = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............
";
    }
}
