namespace AdventOfCode.Y2016.Tests
{
    public class Test04
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(1514, result);
        }

        private const string _input = @"aaaaa-bbb-z-y-x-123[abxyz]
a-b-c-d-e-f-g-h-987[abcde]
not-a-real-room-404[oarel]
totally-real-room-200[decoy]
";
    }
}
