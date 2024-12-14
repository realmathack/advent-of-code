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
            var divisionTestProduct = NumberTheory.LeastCommonMultiple(monkeys.Select(monkey => monkey.TestDivision));
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
                var lines = lineGroup.SplitIntoLines().Select(line => line.Trim()).ToArray();
                var number = int.Parse(lines[0].TrimEnd(':').Split(' ')[1]);
                var items = lines[1].Split(": ")[1].Split(", ").Select(long.Parse).ToArray();
                var operation = GenerateOperation(lines[2].Split(": ")[1]);
                var testDivision = int.Parse(lines[3].Split(' ')[3]);
                var testTrueMonkey = int.Parse(lines[4].Split(' ')[5]);
                var testFalseMonkey = int.Parse(lines[5].Split(' ')[5]);
                monkeys.Add(new(number, items, operation, testDivision, testTrueMonkey, testFalseMonkey));
            }
            return monkeys;
        }

        private static Func<long, long> GenerateOperation(string text)
        {
            var parts = text.Split(' ');
            if (parts[3] == "+")
            {
                var value = int.Parse(parts[4]);
                return item => item + value;
            }
            if (parts[3] == "*")
            {
                if (parts[4] == "old")
                {
                    return item => item * item;
                }
                var value = int.Parse(parts[4]);
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
