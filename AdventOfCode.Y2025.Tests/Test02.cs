namespace AdventOfCode.Y2025.Tests
{
    public class Test02
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(1227775554L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day02();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(4174379265L, result);
        }

        private const string _input = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
    }
}
