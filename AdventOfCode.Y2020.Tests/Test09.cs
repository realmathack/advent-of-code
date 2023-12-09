namespace AdventOfCode.Y2020.Tests
{
    public class Test09
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day09(5);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(127L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day09(5);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(62L, result);
        }

        private const string _input = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576
";
    }
}
