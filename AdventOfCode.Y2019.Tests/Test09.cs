namespace AdventOfCode.Y2019.Tests
{
    public class Test09
    {
        [Theory]
        [InlineData("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", 99L)]
        [InlineData("1102,34915192,34915192,7,4,7,99,0", 1219070632396864L)]
        [InlineData("104,1125899906842624,99", 1125899906842624L)]
        public void TestPart1(string input, long expected)
        {
            var subject = new Day09();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }
    }
}
