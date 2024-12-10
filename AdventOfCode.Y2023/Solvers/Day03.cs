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
            for (int y = 0; y < lines.Length; y++)
            {
                var currentCoords = new List<Coords>();
                var currentNumber = string.Empty;
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (char.IsDigit(lines[y][x]))
                    {
                        currentCoords.Add(new(y, x));
                        currentNumber += lines[y][x];
                        continue;
                    }
                    if (currentNumber != string.Empty)
                    {
                        numbers[currentCoords] = int.Parse(currentNumber);
                        currentNumber = string.Empty;
                        currentCoords = [];
                    }
                    if (lines[y][x] != '.')
                    {
                        parts.Add(new(y, x), lines[y][x]);
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
