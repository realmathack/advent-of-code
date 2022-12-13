using Microsoft.Win32;
using System.Text;

namespace AdventOfCode.Y2016.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return Solve(input).SelectMany(x => x).Count(x => x);
        }

        public override object SolvePart2(string[] input)
        {
            var grid = Solve(input);
            var screen = new StringBuilder();
            for (int row = 0; row < grid.Length; row++)
            {
                screen.AppendLine();
                for (int col = 0; col < grid[row].Length; col++)
                {
                    screen.Append(grid[row][col] ? '#' : '.');
                }
            }
            return screen.ToString();
        }

        private static bool[][] Solve(string[] lines)
        {
            var grid = InitGrid();
            foreach (var instruction in ToInstructions(lines))
            {
                if (instruction.Operation == Operation.Rect)
                {
                    for (int row = 0; row < instruction.B; row++)
                    {
                        for (int col = 0; col < instruction.A; col++)
                        {
                            grid[row][col] = true;
                        }
                    }
                }
                else if (instruction.Operation == Operation.RotateRow)
                {
                    var origValues = grid[instruction.A].Select(col => col).ToArray();
                    for (int i = 0; i < grid[instruction.A].Length; i++)
                    {
                        grid[instruction.A][(i + instruction.B) % grid[instruction.A].Length] = origValues[i];

                    }
                }
                else if (instruction.Operation == Operation.RotateColumn)
                {
                    var origValues = grid.Select(row => row[instruction.A]).ToArray();
                    for (int i = 0; i < grid.Length; i++)
                    {
                        grid[(i + instruction.B) % grid.Length][instruction.A] = origValues[i];

                    }
                }
            }
            return grid;
        }

        private static bool[][] InitGrid()
        {
            var grid = new bool[6][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new bool[50];
            }
            return grid;
        }

        private static List<Instruction> ToInstructions(string[] lines)
        {
            var instructions = new List<Instruction>();
            foreach (var line in lines)
            {
                var split = line.Split('=');
                var parts = split[0].Split(' ');
                if (parts.Length == 2)
                {
                    var dimensions = parts[1].Split('x');
                    instructions.Add(new(Operation.Rect, int.Parse(dimensions[0]), int.Parse(dimensions[1])));
                    continue;
                }
                var operation = (parts[1] == "row") ? Operation.RotateRow : Operation.RotateColumn;
                var numbers = split[1].Split(' ');
                instructions.Add(new(operation, int.Parse(numbers[0]), int.Parse(numbers[2])));
            }
            return instructions;
        }

        private enum Operation { Rect, RotateRow, RotateColumn }
        private record struct Instruction(Operation Operation, int A, int B);
    }
}
