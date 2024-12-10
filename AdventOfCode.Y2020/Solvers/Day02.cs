namespace AdventOfCode.Y2020.Solvers
{
    public class Day02 : SolverWithLines
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

        private static readonly char[] _separator = [' ', '-', ':'];
        private static List<Record> ToRecords(string[] lines)
        {
            var records = new List<Record>();
            foreach (var line in lines)
            {
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                records.Add(new(int.Parse(parts[0]), int.Parse(parts[1]), parts[2][0], parts[3]));
            }
            return records;
        }

        private record class Record(int Lower, int Upper, char Character, string Password);
    }
}
