using System.Text;

namespace AdventOfCode.Y2016.Solvers
{
    public class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var realRoomSectorIds = new List<int>();
            foreach (var line in input)
            {
                var parts = line.Split(new[] { '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                var name = string.Join("", parts[..^2]);
                var top5 = name.Distinct().Select(letter => (letter, count: name.Count(c => c == letter)))
                    .OrderByDescending(x => x.count).ThenBy(x => x.letter).Take(5).Select(x => x.letter).ToList();
                var isValid = true;
                foreach (var letter in parts[^1])
                {
                    if (!top5.Contains(letter))
                    {
                        isValid = false;
                    }
                }
                if (isValid)
                {
                    realRoomSectorIds.Add(int.Parse(parts[^2]));
                }
            }
            return realRoomSectorIds.Sum();
        }

        public override object SolvePart2(string[] input)
        {
            var rooms = new Dictionary<string, int>();
            foreach (var line in input)
            {
                var parts = line.Split(new[] { '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                var name = string.Join(' ', parts[..^2]);
                var sectorId = int.Parse(parts[^2]);
                var shift = sectorId % 26;
                var sb = new StringBuilder();
                foreach (var letter in name)
                {
                    if (letter == ' ')
                    {
                        sb.Append(letter);
                        continue;
                    }
                    sb.Append((char)((letter - 'a' + shift) % 26 + 'a'));
                }
                rooms.Add(sb.ToString(), sectorId);
            }
            return string.Join(Environment.NewLine, rooms.Where(x => x.Key.Contains("north")).Select(x => $"{x.Value}\t{x.Key}"));
        }
    }
}
