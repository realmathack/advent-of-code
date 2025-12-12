namespace AdventOfCode.Y2025.Tests
{
    public class Test10
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day10();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(7, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day10();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(null!, result);
        }

        private const string _input = @"[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
";
    }
}
