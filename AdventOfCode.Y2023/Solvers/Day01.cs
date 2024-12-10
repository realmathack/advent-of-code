namespace AdventOfCode.Y2023.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var total = 0;
            foreach (var line in input)
            {
                var numbers = line.Where(char.IsDigit);
                total += int.Parse($"{numbers.First()}{numbers.Last()}");
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var total = 0;
            foreach (var line in input)
            {
                var numbers = ToDigits(line);
                total += int.Parse($"{numbers.First()}{numbers.Last()}");
            }
            return total;
        }

        private readonly static Dictionary<string, char> _textNumbers = new()
        {
            ["one"] = '1',
            ["two"] = '2',
            ["three"] = '3',
            ["four"] = '4',
            ["five"] = '5',
            ["six"] = '6',
            ["seven"] = '7',
            ["eight"] = '8',
            ["nine"] = '9'
        };
        private static List<char> ToDigits(string line)
        {
            var digits = new List<char>();
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    digits.Add(line[i]);
                    continue;
                }
                var tmp = line[i..];
                foreach (var textNumber in _textNumbers)
                {
                    if (tmp.StartsWith(textNumber.Key))
                    {
                        digits.Add(textNumber.Value);
                        break;
                    }
                }
            }
            return digits;
        }
    }
}
