namespace AdventOfCode.Y2015.Tests
{
    public class Test12
    {
        [Theory]
        [InlineData("[1,2,3]", 6)]
        [InlineData("{\"a\":2,\"b\":4}", 6)]
        [InlineData("[[[3]]]", 3)]
        [InlineData("{\"a\":{\"b\":4},\"c\":-1}", 3)]
        [InlineData("{\"a\":[-1,1]}", 0)]
        [InlineData("[-1,{\"a\":1}]", 0)]
        [InlineData("[]", 0)]
        [InlineData("{}", 0)]
        public void TestPart1(string input, int expected)
        {
            var subject = new Day12();
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("[1,2,3]", 6)]
        [InlineData("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
        [InlineData("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0)]
        [InlineData("[1,\"red\",5]", 6)]
        [InlineData("[1,{\"b\":2,{\"b\":2},\"c\":\"red\",\"b\":2,{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}},3]", 4)]
        public void TestPart2(string input, int expected)
        {
            var subject = new Day12();
            subject.SetInput(input);

            var result = subject.SolvePart2();

            Assert.Equal(expected, result);
        }
    }
}
