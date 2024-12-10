namespace AdventOfCode.Y2018.Tests
{
    public class Test04
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(240, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day04();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(4455, result);
        }

        private const string _input = @"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up
";
    }
}
