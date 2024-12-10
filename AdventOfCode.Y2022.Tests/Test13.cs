namespace AdventOfCode.Y2022.Tests
{
    public class Test13
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(13, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(140, result);
        }

        private const string _input = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]
";
    }
}
