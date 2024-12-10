namespace AdventOfCode.Y2021.Tests
{
    public class Test10
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day10();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(26397, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day10();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(288957L, result);
        }

        private const string _input = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]
";
    }
}
