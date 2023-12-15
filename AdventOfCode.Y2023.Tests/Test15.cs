namespace AdventOfCode.Y2023.Tests
{
    public class Test15
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day15();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(1320, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day15();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(145, result);
        }

        private const string _input = @"rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
    }
}
