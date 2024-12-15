using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Solvers
{
    public partial class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var validPasswords = 0;
            foreach (var record in ToRecords(input))
            {
                var count = record.Password.Count(c => c == record.Character);
                if (count >= record.Lower && count <= record.Upper)
                {
                    validPasswords++;
                }
            }
            return validPasswords;
        }

        public override object SolvePart2(string[] input)
        {
            var validPasswords = 0;
            foreach (var record in ToRecords(input))
            {
                var first = record.Password[record.Lower - 1];
                var second = record.Password[record.Upper - 1];
                if ((first == record.Character && second != record.Character) || (first != record.Character && second == record.Character))
                {
                    validPasswords++;
                }
            }
            return validPasswords;
        }

        private static List<Record> ToRecords(string[] lines)
        {
            var records = new List<Record>();
            foreach (var line in lines)
            {
                var match = RecordRegex().Match(line);
                records.Add(new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value[0], match.Groups[4].Value));
            }
            return records;
        }

        [GeneratedRegex(@"(\d+)-(\d+) (.): (.+)")]
        private static partial Regex RecordRegex();

        private record class Record(int Lower, int Upper, char Character, string Password);
    }
}
