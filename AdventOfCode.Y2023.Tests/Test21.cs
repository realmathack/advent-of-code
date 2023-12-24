namespace AdventOfCode.Y2023.Tests
{
    public class Test21
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day21(6);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(16, result);
        }

        //[Theory]
        //[InlineData(6, 16L)]
        //[InlineData(10, 50L)]
        //[InlineData(50, 1594L)]
        //[InlineData(100, 6536L)]
        //[InlineData(500, 167004L)]
        //[InlineData(1000, 668697L)]
        //[InlineData(5000, 16733044L)]
        //public void TestPart2(int steps, long expected)
        //{
        //    var subject = new Day21(steps);
        //    subject.SetInput(_input);

        //    var result = subject.SolvePart2();

        //    Assert.Equal(expected, result);
        //}

        private const string _input = @"...........
.....###.#.
.###.##..#.
..#.#...#..
....#.#....
.##..S####.
.##..#...#.
.......##..
.##.#.####.
.##..##.##.
...........
";
    }
}
