namespace AdventOfCode.Y2016.Tests
{
    public class Test06
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal("easter", result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day06();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal("advent", result);
        }

        private const string _input = @"eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar
";
    }
}
