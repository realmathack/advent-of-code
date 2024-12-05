namespace AdventOfCode.Y2020.Solvers
{
    public class Day04 : SolverWithLineGroups
    {
        private static readonly char[] _separator = [' ', '\r', '\n'];
        public override object SolvePart1(string[] input)
        {
            var validPassports = 0;
            foreach (var passport in input)
            {
                var fields = ToFields(passport);
                if (fields.Count == 7)
                {
                    validPassports++;
                }
            }
            return validPassports;
        }

        private static readonly string[] _eyeColors = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];
        public override object SolvePart2(string[] input)
        {
            var validPassports = 0;
            foreach (var passport in input)
            {
                var fields = ToFields(passport);
                if (fields.Count == 7 &&
                    (int.TryParse(fields["byr"], out var birthYear) && birthYear >= 1920 && birthYear <= 2002) &&
                    (int.TryParse(fields["iyr"], out var issueYear) && issueYear >= 2010 && issueYear <= 2020) &&
                    (int.TryParse(fields["eyr"], out var expirationYear) && expirationYear >= 2020 && expirationYear <= 2030) &&
                    (int.TryParse(fields["hgt"][..^2], out var height) && (
                        (fields["hgt"][^2..] == "cm" && height >= 150 && height <= 193) ||
                        (fields["hgt"][^2..] == "in" && height >= 59 && height <= 76)
                    )) &&
                    (fields["hcl"].Length == 7 && fields["hcl"][0] == '#' && int.TryParse(fields["hcl"][1..6], System.Globalization.NumberStyles.HexNumber, null, out var hairColor)) &&
                    _eyeColors.Contains(fields["ecl"]) &&
                    (fields["pid"].Length == 9) && int.TryParse(fields["pid"], out var passportId))
                {
                    validPassports++;
                }
            }
            return validPassports;
        }

        private static Dictionary<string ,string> ToFields(string passport)
        {
            var fields = new Dictionary<string, string>();
            foreach (var field in passport.Split(_separator, StringSplitOptions.RemoveEmptyEntries))
            {
                var (name, value) = field.SplitInTwo(':');
                if (name != "cid")
                {
                    fields.Add(name, value);
                }
            }
            return fields;
        }
    }
}
