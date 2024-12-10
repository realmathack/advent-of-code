namespace AdventOfCode.Y2020.Tests
{
    public class Test10
    {
        [Theory]
        [InlineData(_input1, 35)]
        [InlineData(_input2, 220)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day10();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(_input1, 8L)]
        [InlineData(_input2, 19208L)]
        public void TestPart2(string input, long expected)
        {
            var subject = new Day10();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }

        private const string _input1 = @"16
10
15
5
1
11
7
19
6
12
4
";
        private const string _input2 = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3
";
    }
}
