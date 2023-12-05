namespace AdventOfCode.Y2019.Tests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input1);

            var result = subject.SolvePart1();

            Assert.Equal(42, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06();
            subject.SetInput(_input2);

            var result = subject.SolvePart2();

            Assert.Equal(4, result);
        }

        private const string _input1 = @"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L
";
        private const string _input2 = @"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L
K)YOU
I)SAN
";
    }
}
