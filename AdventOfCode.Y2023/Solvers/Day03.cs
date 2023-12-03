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
                if (number.Key.SelectMany(x => x.Adjacents).Distinct().Any(parts.ContainsKey))
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
            foreach (var part in parts.Where(x => x.Value == '*'))
            {
                var partNumbers = numbers.Where(x => x.Key.Any(part.Key.Adjacents.Contains)).Select(x => x.Value).ToArray();
                if (partNumbers.Length == 2)
                {
                    sum += partNumbers[0] * partNumbers[1];
                }
            }
            return sum;
        }

        private static (Dictionary<List<Coords>, int> numbers, Dictionary<Coords, char> parts) ToNumbersAndParts(string[] input)
        {
            var numbers = new Dictionary<List<Coords>, int>();
            var parts = new Dictionary<Coords, char>();
            for (int row = 0; row < input.Length; row++)
            {
                var currentCoords = new List<Coords>();
                var currentNumber = string.Empty;
                for (int col = 0; col < input[row].Length; col++)
                {
                    if (char.IsDigit(input[row][col]))
                    {
                        currentCoords.Add(new(row, col));
                        currentNumber += input[row][col];
                        continue;
                    }
                    if (currentNumber != string.Empty)
                    {
                        numbers[currentCoords] = int.Parse(currentNumber);
                        currentNumber = string.Empty;
                        currentCoords = [];
                    }
                    if (input[row][col] != '.')
                    {
                        parts.Add(new(row, col), input[row][col]);
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
