namespace AdventOfCode.Y2023.Tests
{
    public class Test23
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day23();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(94, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day23();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(154, result);
        }

        private const string _input = @"#.#####################
#.......#########...###
#######.#########.#.###
###.....#.>.>.###.#.###
###v#####.#v#.###.#.###
###.>...#.#.#.....#...#
###v###.#.#.#########.#
###...#.#.#.......#...#
#####.#.#.#######.#.###
#.....#.#.#.......#...#
#.#####.#.#.#########v#
#.#...#...#...###...>.#
#.#.#v#######v###.###v#
#...#.>.#...>.>.#.###.#
#####v#.#.###v#.#.###.#
#.....#...#...#.#.#...#
#.#########.###.#.#.###
#...###...#...#...#.###
###.###.#.###v#####v###
#...#...#.#.>.>.#.>.###
#.###.###.#.###.#.#v###
#.....###...###...#...#
#####################.#
";
    }
}
