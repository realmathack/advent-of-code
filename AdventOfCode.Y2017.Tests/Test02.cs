namespace AdventOfCode.Y2017.Tests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();
            subject.SetInput(_input1);

            var result = subject.SolvePart1();

            Assert.Equal(18, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();
            subject.SetInput(_input2);

            var result = subject.SolvePart2();

            Assert.Equal(9, result);
        }

        private const string _input1 = @"5	1	9	5
7	5	3
2	4	6	8";
        private const string _input2 = @"5	9	2	8
9	4	7	3
3	8	6	5";
    }
}
