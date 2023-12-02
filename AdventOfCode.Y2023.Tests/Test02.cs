namespace AdventOfCode.Y2023.Tests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(8, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(2286, result);
        }

        private const string _input = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
";
    }
}
