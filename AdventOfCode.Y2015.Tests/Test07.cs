namespace AdventOfCode.Y2015.Tests
{
    public class Test07
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal((ushort)65412, result);
        }

        private const string _input = @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> a
NOT y -> i
"; // Changed h to a for testing purpose
    }
}
