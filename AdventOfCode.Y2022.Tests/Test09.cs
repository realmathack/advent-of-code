namespace AdventOfCode.Y2022.Tests
{
    public class Test09
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(13, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day09();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(1, result);
        }

        [Fact]
        public void TestPart2Larger()
        {
            var subject = new Day09();
            subject.SetInput(_inputLarger);

            var result = subject.SolvePart2();

            Assert.Equal(36, result);
        }

        private const string _input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2
";
        private const string _inputLarger = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20
";
    }
}
