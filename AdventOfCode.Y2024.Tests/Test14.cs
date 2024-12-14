namespace AdventOfCode.Y2024.Tests
{
    public class Test14
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day14(11, 7);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(12, result);
        }

        private const string _input = @"p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3
";
    }
}
