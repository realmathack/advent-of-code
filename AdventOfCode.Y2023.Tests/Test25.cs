namespace AdventOfCode.Y2023.Tests
{
    public class Test25
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day25();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(54, result);
        }

        private const string _input = @"jqt: rhn xhk nvd
rsh: frs pzl lsr
xhk: hfx
cmg: qnr nvd lhk bvb
rhn: xhk bvb hfx
bvb: xhk hfx
pzl: lsr hfx nvd
qnr: nvd
ntq: jqt hfx bvb xhk
nvd: lhk
lsr: lhk
rzs: qnr cmg lsr rsh
frs: qnr lhk lsr
";
    }
}
