namespace AdventOfCode.Y2018.Tests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(4, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day03();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(3, result);
        }

        private const string _input = @"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2
";
    }
}
