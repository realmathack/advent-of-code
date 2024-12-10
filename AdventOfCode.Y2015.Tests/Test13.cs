namespace AdventOfCode.Y2015.Tests
{
    public class Test13
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(330, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day13();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(286, result);
        }

        private const string _input = @"Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
";
    }
}
