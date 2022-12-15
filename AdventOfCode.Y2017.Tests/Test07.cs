namespace AdventOfCode.Y2017.Tests
{
    public class Test07
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal("tknk", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(60, result);
        }

        private const string _input = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)
";
    }
}
