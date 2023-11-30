namespace AdventOfCode.Y2020.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var validPasswords = 0;
            foreach (var line in ToLines(input))
            {
                var count = line.Password.Count(c => c == line.Character);
                if (count >= line.Lower && count <= line.Upper)
                {
                    validPasswords++;
                }
            }
            return validPasswords;
        }

        public override object SolvePart2(string[] input)
        {
            var validPasswords = 0;
            foreach (var line in ToLines(input))
            {
                var first = line.Password[line.Lower - 1];
                var second = line.Password[line.Upper - 1];
                if ((first == line.Character && second != line.Character) || (first != line.Character && second == line.Character))
                {
                    validPasswords++;
                }
            }
            return validPasswords;
        }

        private static readonly char[] _separator = [' ', '-', ':'];
        private static List<Line> ToLines(string[] input)
        {
            var lines = new List<Line>();
            foreach (var line in input)
            {
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                lines.Add(new(int.Parse(parts[0]), int.Parse(parts[1]), parts[2][0], parts[3]));
            }
            return lines;
        }

        private record class Line(int Lower, int Upper, char Character, string Password);
    }
}
