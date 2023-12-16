namespace AdventOfCode.Y2023.Tests
{
    public class Test12
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(21L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(525152L, result);
        }

        private const string _input = @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1
";
    }
}
