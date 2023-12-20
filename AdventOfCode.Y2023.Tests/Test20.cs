namespace AdventOfCode.Y2023.Tests
{
    public class Test20
    {
        [Theory]
        [InlineData(_input1, 32000000L)]
        [InlineData(_input2, 11687500L)]
        public void TestPart1(string input, long expected)
        {
            var subject = new Day20();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        private const string _input1 = @"broadcaster -> a, b, c
%a -> b
%b -> c
%c -> inv
&inv -> a
";
        private const string _input2 = @"broadcaster -> a
%a -> inv, con
&inv -> b
%b -> con
&con -> output
";
    }
}
