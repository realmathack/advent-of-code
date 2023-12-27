namespace AdventOfCode.Y2019.Tests
{
    public class Test08
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day08(3, 2);
            subject.SetInput("123456789012");

            var result = subject.SolvePart1();

            Assert.Equal(1, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day08(2, 2, false);
            subject.SetInput("0222112222120000");

            var result = subject.SolvePart2();

            Assert.Equal(@"
.#
#.", result);
        }
    }
}
