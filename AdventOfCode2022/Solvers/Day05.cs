using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solvers
{
    public class Day05 : IBaseSolver
    {
        public string SolvePart1(string input)
        {
            var data = ParseInput(input);
            foreach (var move in data.Moves)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = data.Stacks[move.SourceStack - 1].Pop();
                    data.Stacks[move.DestinationStack - 1].Push(crate);
                }
            }
            var result = data.Stacks.Select(s => s.Peek()).ToList();
            return string.Join("", result);
        }

        public string SolvePart2(string input)
        {
            var data = ParseInput(input);
            var moverStack = new Stack<char>();
            foreach (var move in data.Moves)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = data.Stacks[move.SourceStack - 1].Pop();
                    moverStack.Push(crate);
                }
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = moverStack.Pop();
                    data.Stacks[move.DestinationStack - 1].Push(crate);
                }
            }
            var result = data.Stacks.Select(s => s.Peek()).ToList();
            return string.Join("", result);
        }

        private static StacksAndMoves ParseInput(string input)
        {
            var parts = input.Split(Environment.NewLine + Environment.NewLine);
            // stacks
            var stacks = parts[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var identifiers = stacks.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var stackCount = identifiers.Length;
            var result = new StacksAndMoves(stackCount);
            for (int i = stacks.Length - 2; i >= 0; i--)
            {
                for (int stack = 0; stack < stackCount; stack++)
                {
                    var crate = stacks[i].Substring(stack * 4, 3);
                    if (crate[1] != ' ')
                    {
                        result.Stacks[stack].Push(crate[1]);
                    }
                }
            }
            // moves
            var moves = parts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var regex = new Regex(@"move (?<Count>\d+) from (?<Source>\d+) to (?<Destination>\d+)");
            foreach (var line in moves)
            {
                var match = regex.Match(line);
                if (!match.Success)
                {
                    throw new InvalidOperationException($"Unknown move: '{line}'");
                }
                var move = new Move(int.Parse(match.Groups["Count"].Value), int.Parse(match.Groups["Source"].Value), int.Parse(match.Groups["Destination"].Value));
                result.Moves.Add(move);
            }
            return result;
        }

        private class StacksAndMoves
        {
            public StacksAndMoves(int stackCount)
            {
                Stacks = new Stack<char>[stackCount];
                for (int i = 0; i < stackCount; i++)
                {
                    Stacks[i] = new Stack<char>();
                }
                Moves = new List<Move>();
            }

            public Stack<char>[] Stacks { get; set; }
            public List<Move> Moves { get; set; }
        }

        private class Move
        {
            public Move(int count, int sourceStack, int destinationStack)
            {
                Count = count;
                SourceStack = sourceStack;
                DestinationStack = destinationStack;
            }

            public int Count { get; set; }
            public int SourceStack { get; set; }
            public int DestinationStack { get; set; }
        }
    }
}
