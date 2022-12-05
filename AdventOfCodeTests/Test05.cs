namespace AdventOfCodeTests
{
    public class Test05
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day05();

            var result = subject.SolvePart1(_input);

            Assert.Equal("CMZ", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day05();

            var result = subject.SolvePart2(_input);

            Assert.Equal("MCD", result);
        }

        private const string _input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2
";
    }
}
