﻿namespace AdventOfCode.Y2017.Tests
{
    public class Test05
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(5, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(10, result);
        }

        private const string _input = @"0
3
0
1
-3
";
    }
}
