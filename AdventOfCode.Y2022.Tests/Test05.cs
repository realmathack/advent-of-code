namespace AdventOfCode.Y2022.Tests
{
    public class Test05
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal("CMZ", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day05();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

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
