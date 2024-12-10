namespace AdventOfCode.Y2023.Tests
{
    public class Test16
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day16();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(46, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day16();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(51, result);
        }

        private const string _input = @".|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|....
";
    }
}
