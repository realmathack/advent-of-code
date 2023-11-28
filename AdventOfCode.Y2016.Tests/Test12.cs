namespace AdventOfCode.Y2016.Tests
{
    public class Test12
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day12();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(42, result);
        }

        private const string _input = @"cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a
";
    }
}
