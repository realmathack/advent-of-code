namespace AdventOfCode.Y2016.Tests
{
    public class Test11
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(11, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(null!, result);
        }

        private const string _input = @"The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
The second floor contains a hydrogen generator.
The third floor contains a lithium generator.
The fourth floor contains nothing relevant.
";
    }
}
