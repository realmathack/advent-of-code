namespace AdventOfCode.Y2015.Tests
{
    public class Test15
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day15();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(62842880, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day15();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(57600000, result);
        }

        private const string _input = @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
";
    }
}
