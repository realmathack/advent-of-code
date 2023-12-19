namespace AdventOfCode.Y2023.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var sum = 0;
            var (numbers, parts) = ToNumbersAndParts(input);
            foreach (var number in numbers)
            {
                if (number.Key.SelectMany(coord => coord.Adjacents).Distinct().Any(parts.ContainsKey))
                {
                    sum += number.Value;
                }
            }
            return sum;
        }

        public override object SolvePart2(string[] input)
        {
            var sum = 0;
            var (numbers, parts) = ToNumbersAndParts(input);
            foreach (var part in parts.Where(part => part.Value == '*'))
            {
                var partNumbers = numbers.Where(number => number.Key.Any(part.Key.Adjacents.Contains)).Select(number => number.Value).ToArray();
                if (partNumbers.Length == 2)
                {
                    sum += partNumbers[0] * partNumbers[1];
                }
            }
            return sum;
        }

        private static (Dictionary<List<Coords>, int> Numbers, Dictionary<Coords, char> Parts) ToNumbersAndParts(string[] lines)
        {
            var numbers = new Dictionary<List<Coords>, int>();
            var parts = new Dictionary<Coords, char>();
            for (int row = 0; row < lines.Length; row++)
            {
                var currentCoords = new List<Coords>();
                var currentNumber = string.Empty;
                for (int col = 0; col < lines[row].Length; col++)
                {
                    if (char.IsDigit(lines[row][col]))
                    {
                        currentCoords.Add(new(row, col));
                        currentNumber += lines[row][col];
                        continue;
                    }
                    if (currentNumber != string.Empty)
                    {
                        numbers[currentCoords] = int.Parse(currentNumber);
                        currentNumber = string.Empty;
                        currentCoords = [];
                    }
                    if (lines[row][col] != '.')
                    {
                        parts.Add(new(row, col), lines[row][col]);
                    }
                }
                if (currentNumber != string.Empty)
                {
                    numbers[currentCoords] = int.Parse(currentNumber);
                }
            }
            return (numbers, parts);
        }
    }
}
