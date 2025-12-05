namespace AdventOfCode.Y2025.Tests
{
    public class Test04
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(13, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(43, result);
        }

        private const string _input = @"..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.
";
    }
}
