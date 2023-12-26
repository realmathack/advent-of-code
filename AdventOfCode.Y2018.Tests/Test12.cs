namespace AdventOfCode.Y2018.Tests
{
    public class Test12
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(325, result);
        }

        private const string _input = @"initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #
";
    }
}
