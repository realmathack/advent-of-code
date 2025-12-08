namespace AdventOfCode.Y2025.Tests
{
    public class Test08
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day08(10);
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(40, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day08();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(25272L, result);
        }

        private const string _input = @"162,817,812
57,618,57
906,360,560
592,479,940
352,342,300
466,668,158
542,29,236
431,825,988
739,650,466
52,470,668
216,146,977
819,987,18
117,168,530
805,96,715
346,949,466
970,615,88
941,993,340
862,61,35
984,92,344
425,690,689
";
    }
}
