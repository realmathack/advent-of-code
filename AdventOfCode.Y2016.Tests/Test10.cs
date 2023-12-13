namespace AdventOfCode.Y2016.Tests
{
    public class Test10
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day10(5, 2);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(2, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day10();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(30, result);
        }

        private const string _input = @"value 5 goes to bot 2
bot 2 gives low to bot 1 and high to bot 0
value 3 goes to bot 1
bot 1 gives low to output 1 and high to bot 0
bot 0 gives low to output 2 and high to output 0
value 2 goes to bot 2
";
    }
}
