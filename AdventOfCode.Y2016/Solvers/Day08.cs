﻿namespace AdventOfCode.Y2016.Solvers
{
    public class Day08 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ExecuteInstructions(input).SelectMany(row => row).Count(pixel => pixel);
        public override object SolvePart2(string[] input) => new Screen(ExecuteInstructions(input)).ReadScreen();

        private static bool[][] ExecuteInstructions(string[] lines)
        {
            var grid = InitGrid();
            foreach (var instruction in ToInstructions(lines))
            {
                if (instruction.Operation == Operation.Rect)
                {
                    for (int y = 0; y < instruction.B; y++)
                    {
                        for (int x = 0; x < instruction.A; x++)
                        {
                            grid[y][x] = true;
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
                    var (width, height) = parts[1].SplitInTwo('x');
                    instructions.Add(new(Operation.Rect, int.Parse(width), int.Parse(height)));
                    continue;
                }
                var operation = (parts[1] == "row") ? Operation.RotateRow : Operation.RotateColumn;
                var (index, shift) = split[1].SplitInTwo(" by ");
                instructions.Add(new(operation, int.Parse(index), int.Parse(shift)));
            }
            return instructions;
        }

        private enum Operation { Rect, RotateRow, RotateColumn }
        private record class Instruction(Operation Operation, int A, int B);
    }
}
