namespace AdventOfCode.Y2023.Solvers
{
    public class Day15 : SolverWithText
    {
        public override object SolvePart1(string input) => input.Split(',').Sum(CalculateHash);

        public override object SolvePart2(string input)
        {
            var boxes = new List<Lens>[256];
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = [];
            }
            var instructions = input.Split(',');
            foreach (var instruction in instructions)
            {
                if (instruction.EndsWith('-'))
                {
                    var label = instruction[..^1];
                    var current = CalculateHash(label);
                    if (boxes[current].Any(lens => lens.Label == label))
                    {
                        boxes[current].Remove(boxes[current].First(lens => lens.Label == label));
                    }
                }
                else
                {
                    var (label, focalStrength) = instruction.SplitInTwo('=');
                    var newLens = new Lens(label, int.Parse(focalStrength));
                    var current = CalculateHash(newLens.Label);
                    if (boxes[current].Any(lens => lens.Label == newLens.Label))
                    {
                        var index = boxes[current].IndexOf(boxes[current].First(lens => lens.Label == newLens.Label));
                        boxes[current][index] = newLens;
                    }
                    else
                    {
                        boxes[current].Add(newLens);
                    }
                }
            }
            var sum = 0;
            for (int i = 0; i < boxes.Length; i++)
            {
                sum += CaculateBoxFocalStrength(i, boxes[i]);
            }
            return sum;
        }

        private static int CaculateBoxFocalStrength(int boxIndex, List<Lens> lenses)
        {
            var sum = 0;
            for (int i = 0; i < lenses.Count; i++)
            {
                sum += (boxIndex + 1) * (i + 1) * lenses[i].FocalStrength;
            }
            return sum;
        }

        private static int CalculateHash(string text)
        {
            var hash = 0;
            foreach (var character in text)
            {
                hash += character;
                hash *= 17;
                hash %= 256;
            }
            return hash;
        }

        private readonly record struct Lens(string Label, int FocalStrength);
    }
}
