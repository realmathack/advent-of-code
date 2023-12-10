namespace AdventOfCode.Y2018.Tests
{
    public class Test07
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal("CABDFE", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day07(2, 0);
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(15, result);
        }

        private const string _input = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.
";
    }
}
