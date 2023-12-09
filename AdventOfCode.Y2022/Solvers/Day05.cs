namespace AdventOfCode.Y2022.Solvers
{
    public class Day05 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var stacks = ToStacks(input[0]);
            var moves = ToMoves(input[1]);
            foreach (var move in moves)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = stacks[move.Source - 1].Pop();
                    stacks[move.Destination - 1].Push(crate);
                }
            }
            var result = stacks.Select(stack => stack.Peek()).ToList();
            return string.Concat(result);
        }

        public override object SolvePart2(string[] input)
        {
            var stacks = ToStacks(input[0]);
            var moves = ToMoves(input[1]);
            var moverStack = new Stack<char>();
            foreach (var move in moves)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = stacks[move.Source - 1].Pop();
                    moverStack.Push(crate);
                }
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = moverStack.Pop();
                    stacks[move.Destination - 1].Push(crate);
                }
            }
            var result = stacks.Select(stack => stack.Peek()).ToList();
            return string.Concat(result);
        }

        private static Stack<char>[] ToStacks(string input)
        {
            var stacks = input.SplitIntoLines();
            var stackCount = (stacks[^1].Length + 1) / 4;
            var result = new Stack<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                result[i] = new Stack<char>(stacks.Length);
            }
            for (int i = stacks.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < stackCount; j++)
                {
                    var crate = stacks[i][j * 4 + 1];
                    if (crate != ' ')
                    {
                        result[j].Push(crate);
                    }
                }
            }
            return result;
        }

        private static List<Move> ToMoves(string moves)
        {
            var result = new List<Move>();
            foreach (var line in moves.SplitIntoLines())
            {
                var parts = line.Split(' ');
                result.Add(new Move(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5])));
            }
            return result;
        }

        private readonly record struct Move(int Count, int Source, int Destination);
    }
}
