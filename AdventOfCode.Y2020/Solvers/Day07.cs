namespace AdventOfCode.Y2020.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var bags = ToBags(input);
            var containing = new HashSet<string>();
            var queue = new Queue<Bag>();
            queue.Enqueue(bags["shiny gold"]);
            while (queue.TryDequeue(out var current))
            {
                foreach (var container in bags.Values.Where(bag => bag.Bags.ContainsKey(current.Name)))
                {
                    if (containing.Add(container.Name))
                    {
                        queue.Enqueue(container);
                    }
                }
            }
            return containing.Count;
        }

        public override object SolvePart2(string[] input)
        {
            var bags = ToBags(input);
            var sum = 0;
            var queue = new Queue<Bag>();
            queue.Enqueue(bags["shiny gold"]);
            while (queue.TryDequeue(out var current))
            {
                foreach (var bag in current.Bags)
                {
                    for (int i = 0; i < bag.Value; i++)
                    {
                        sum++;
                        queue.Enqueue(bags[bag.Key]);
                    }
                }
            }
            return sum;
        }

        private static readonly string _contain = " contain ";
        private static Dictionary<string, Bag> ToBags(string[] lines)
        {
            var bags = new Dictionary<string, Bag>();
            foreach (var line in lines)
            {
                var pos = line.IndexOf(" bags ");
                var bag = new Bag(line[..pos]);
                bags.Add(bag.Name, bag);
                pos = line.IndexOf(_contain);
                var contain = line[(pos + _contain.Length)..^1];
                if (contain == "no other bags")
                {
                    continue;
                }
                foreach (var part in contain.Split(", "))
                {
                    pos = part.LastIndexOf(' ');
                    bag.Bags.Add(part[2..pos], int.Parse(part[..1]));
                }
            }
            return bags;
        }

        private record class Bag(string Name)
        {
            public Dictionary<string, int> Bags { get; } = [];
        }
    }
}
