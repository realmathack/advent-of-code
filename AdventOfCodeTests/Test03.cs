namespace AdventOfCodeTests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();

            var result = subject.SolvePart1(_input);

            Assert.Equal("157", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day03();

            var result = subject.SolvePart2(_input);

            Assert.Equal("70", result);
        }

        private const string _input = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw
";
    }
}
