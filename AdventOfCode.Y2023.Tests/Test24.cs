namespace AdventOfCode.Y2023.Tests
{
    public class Test24
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day24(7L, 27L);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(2, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day24();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(47, result);
        }

        private const string _input = @"19, 13, 30 @ -2,  1, -2
18, 19, 22 @ -1, -1, -2
20, 25, 34 @ -2, -2, -4
12, 31, 28 @ -1, -2, -1
20, 19, 15 @  1, -5, -3
";
    }
}
