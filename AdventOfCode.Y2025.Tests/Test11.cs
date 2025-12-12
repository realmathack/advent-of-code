namespace AdventOfCode.Y2025.Tests
{
    public class Test11
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day11();
            subject.SetInput(_input1);

            var result = subject.SolvePart1();

            Assert.Equal(5L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day11();
            subject.SetInput(_input2);

            var result = subject.SolvePart2();

            Assert.Equal(2L, result);
        }

        private const string _input1 = @"aaa: you hhh
you: bbb ccc
bbb: ddd eee
ccc: ddd eee fff
ddd: ggg
eee: out
fff: out
ggg: out
hhh: ccc fff iii
iii: out
";
        private const string _input2 = @"svr: aaa bbb
aaa: fft
fft: ccc
bbb: tty
tty: ccc
ccc: ddd eee
ddd: hub
hub: fff
eee: dac
dac: fff
fff: ggg hhh
ggg: out
hhh: out
";
    }
}
