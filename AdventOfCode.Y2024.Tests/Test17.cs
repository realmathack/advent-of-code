namespace AdventOfCode.Y2024.Tests
{
    public class Test17
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day17();
            subject.SetInput(_input1);

            var result = subject.SolvePart1();

            Assert.Equal("4,6,3,5,6,3,5,2,1,0", result);
        }

        [Fact(Skip = "Not representitive of the actual problem")]
        public void TestPart2()
        {
            var subject = new Day17();
            subject.SetInput(_input2);

            var result = subject.SolvePart2();

            Assert.Equal(117440L, result);
        }

        private const string _input1 = @"Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0
";
        private const string _input2 = @"Register A: 2024
Register B: 0
Register C: 0

Program: 0,3,5,4,3,0
";
    }
}
