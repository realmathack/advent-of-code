namespace AdventOfCode.Y2024.Tests
{
    public class Test03
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day03();
            subject.SetInput(_input1);

            var result = subject.SolvePart1();

            Assert.Equal(161, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day03();
            subject.SetInput(_input2);

            var result = subject.SolvePart2();

            Assert.Equal(48, result);
        }

        private const string _input1 = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        private const string _input2 = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
    }
}
