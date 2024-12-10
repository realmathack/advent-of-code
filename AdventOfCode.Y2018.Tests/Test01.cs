namespace AdventOfCode.Y2018.Tests
{
    public class Test01
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(3, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day01();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(2, result);
        }

        private const string _input = @"+1
-2
+3
+1
";
    }
}
