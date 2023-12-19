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
            var topCrates = stacks.Select(stack => stack.Peek()).ToList();
            return string.Concat(topCrates);
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
            var topCrates = stacks.Select(stack => stack.Peek()).ToList();
            return string.Concat(topCrates);
        }

        private static Stack<char>[] ToStacks(string section)
        {
            var lines = section.SplitIntoLines();
            var stackCount = (lines[^1].Length + 1) / 4;
            var stacks = new Stack<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                stacks[i] = new Stack<char>(lines.Length);
            }
            for (int i = lines.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < stackCount; j++)
                {
                    var crate = lines[i][j * 4 + 1];
                    if (crate != ' ')
                    {
                        stacks[j].Push(crate);
                    }
                }
            }
            return stacks;
        }

        private static List<Move> ToMoves(string section)
        {
            var moves = new List<Move>();
            foreach (var line in section.SplitIntoLines())
            {
                var parts = line.Split(' ');
                moves.Add(new Move(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5])));
            }
            return moves;
        }

        private readonly record struct Move(int Count, int Source, int Destination);
    }
}
