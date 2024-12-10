namespace AdventOfCode.Y2022.Tests
{
    public class Test08
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(21, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(8, result);
        }

        private const string _input = @"30373
25512
65332
33549
35390
";
    }
}
