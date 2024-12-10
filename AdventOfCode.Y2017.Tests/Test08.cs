namespace AdventOfCode.Y2017.Tests
{
    public class Test08
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(1, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(10, result);
        }

        private const string _input = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10
";
    }
}
