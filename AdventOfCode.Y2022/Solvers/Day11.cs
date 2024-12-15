namespace AdventOfCode.Y2022.Solvers
{
    public class Day11 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var monkeys = ToMonkeys(input);
            var inspections = new long[monkeys.Count];
            Array.Fill(inspections, 0);
            for (int round = 0; round < 20; round++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        inspections[monkey.Number]++;
                        item = monkey.Operation(item) / 3;
                        var target = (item % monkey.TestDivision == 0) ? monkey.TestTrueMonkey : monkey.TestFalseMonkey;
                        monkeys[target].Items.Enqueue(item);
                    }
                }
            }
            return inspections.OrderByDescending(inspection => inspection).Take(2).Product();
        }

        public override object SolvePart2(string[] input)
        {
            var monkeys = ToMonkeys(input);
            var inspections = new long[monkeys.Count];
            Array.Fill(inspections, 0);
            var divisionTestProduct = NumberTheory.LCM(monkeys.Select(monkey => monkey.TestDivision));
            for (int round = 0; round < 10_000; round++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        inspections[monkey.Number]++;
                        item = monkey.Operation(item) % divisionTestProduct;
                        var target = (item % monkey.TestDivision == 0) ? monkey.TestTrueMonkey : monkey.TestFalseMonkey;
                        monkeys[target].Items.Enqueue(item);
                    }
                }
            }
            return inspections.OrderByDescending(inspection => inspection).Take(2).Product();
        }

        private static List<Monkey> ToMonkeys(string[] lineGroups)
        {
            var monkeys = new List<Monkey>();
            foreach (var lineGroup in lineGroups)
            {
                var lines = lineGroup.SplitIntoLines();
                var number = int.Parse(lines[0][7..^1]);
                var items = lines[1][18..].Split(", ").Select(long.Parse).ToArray();
                var operation = GenerateOperation(lines[2][23..]);
                var testDivision = int.Parse(lines[3][21..]);
                var testTrueMonkey = int.Parse(lines[4][29..]);
                var testFalseMonkey = int.Parse(lines[5][30..]);
                monkeys.Add(new(number, items, operation, testDivision, testTrueMonkey, testFalseMonkey));
            }
            return monkeys;
        }

        private static Func<long, long> GenerateOperation(string text)
        {
            if (text[0] == '+')
            {
                var value = long.Parse(text[2..]);
                return item => item + value;
            }
            if (text[0] == '*')
            {
                if (text[2..] == "old")
                {
                    return item => item * item;
                }
                var value = long.Parse(text[2..]);
                return item => item * value;
            }
            return item => item;
        }

        private class Monkey(int number, IEnumerable<long> items, Func<long, long> operation, int testDivision, int testTrueMonkey, int testFalseMonkey)
        {
            public int Number { get; init; } = number;
            public Queue<long> Items { get; init; } = new Queue<long>(items);
            public Func<long, long> Operation { get; init; } = operation;
            public int TestDivision { get; init; } = testDivision;
            public int TestTrueMonkey { get; init; } = testTrueMonkey;
            public int TestFalseMonkey { get; init; } = testFalseMonkey;
        }
    }
}
