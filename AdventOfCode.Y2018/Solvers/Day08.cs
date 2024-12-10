namespace AdventOfCode.Y2018.Solvers
{
    public class Day08 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var root = ToTree(input);
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            var sum = 0;
            while (queue.TryDequeue(out var current))
            {
                sum += current.Metadata.Sum();
                current.Children.ForEach(queue.Enqueue);
            }
            return sum;
        }

        public override object SolvePart2(string input)
        {
            var root = ToTree(input);
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            var sum = 0;
            while (queue.TryDequeue(out var current))
            {
                if (current.Children.Count == 0)
                {
                    sum += current.Metadata.Sum();
                    continue;
                }
                foreach (var number in current.Metadata.Where(number => number <= current.Children.Count))
                {
                    queue.Enqueue(current.Children[number - 1]);
                }
            }
            return sum;
        }

        private static Node ToTree(string data)
        {
            var numbers = data.Split(' ').Select(int.Parse).ToArray();
            var stack = new Stack<Node>();
            var root = new Node(numbers[0], numbers[1]);
            stack.Push(root);
            var index = 2;
            while (index < numbers.Length)
            {
                var current = stack.Peek();
                if (current.ChildCount != current.Children.Count)
                {
                    var child = new Node(numbers[index++], numbers[index++]);
                    current.Children.Add(child);
                    stack.Push(child);
                }
                else if (current.MetadataCount != current.Metadata.Count)
                {
                    current.Metadata.Add(numbers[index++]);
                    if (current.MetadataCount == current.Metadata.Count)
                    {
                        stack.Pop();
                    }
                }
            }
            return root;
        }

        private record class Node(int ChildCount, int MetadataCount)
        {
            public List<Node> Children { get; } = [];
            public List<int> Metadata { get; } = [];
        }
    }
}
