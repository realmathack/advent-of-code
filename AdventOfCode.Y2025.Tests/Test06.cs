namespace AdventOfCode.Y2025.Tests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(4277556L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(3263827L, result);
        }

        private const string _input = @"123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  
";
    }
}
