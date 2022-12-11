namespace AdventOfCode.Y2022.Tests
{
    public class Test11
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(10605L, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day11();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(2713310158L, result);
        }

        private const string _input = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1
";
    }
}
