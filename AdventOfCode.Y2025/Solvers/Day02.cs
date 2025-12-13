using Range = AdventOfCode.Range<long>;

namespace AdventOfCode.Y2025.Solvers
{
    public class Day02 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var total = 0L;
            var ranges = input.Split(',').Select(Range.Parse);
            foreach (var range in ranges)
            {
                for (long i = range.Start; i <= range.End; i++)
                {
                    var text = i.ToString();
                    if (text.Length % 2 != 0)
                    {
                        continue;
                    }
                    var mid = text.Length / 2;
                    if (text[0..mid] == text[mid..])
                    {
                        total += i;
                    }
                }
            }
            return total;
        }

        public override object SolvePart2(string input)
        {
            var total = 0L;
            var ranges = input.Split(',').Select(Range.Parse);
            foreach (var range in ranges)
            {
                for (long i = range.Start; i <= range.End; i++)
                {
                    if (IsInvalidId(i.ToString()))
                    {
                        total += i;
                    }
                }
            }
            return total;
        }

        private static bool IsInvalidId(string id)
        {
            var mid = id.Length / 2;
            for (int length = 1; length <= mid; length++)
            {
                if (id.Length % length != 0)
                {
                    continue;
                }
                var isValid = true;
                var sequence = id[..length];
                for (int i = 0; i < id.Length / length; i++)
                {
                    if (id[(i * length)..((i + 1) * length)] != sequence)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
