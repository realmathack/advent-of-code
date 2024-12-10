﻿namespace AdventOfCode.Y2015.Tests
{
    public class Test18
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day18(4);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(4, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day18(5);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(17, result);
        }

        private const string _input = @".#.#.#
...##.
#....#
..#...
#.#..#
####..
";
    }
}
