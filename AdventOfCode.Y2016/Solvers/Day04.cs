using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016.Solvers
{
    public partial class Day04 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var realRoomSectorIds = new List<int>();
            foreach (var line in input)
            {
                var match = RoomRegex().Match(line);
                var name = match.Groups[1].Value.Replace("-", string.Empty);
                var top5 = name.GroupBy(c => c).Select(g => (g.Key, Count: g.Count())).OrderByDescending(g => g.Count).ThenBy(g => g.Key).Take(5).Select(g => g.Key).ToHashSet();
                if (match.Groups[3].Value.All(top5.Contains))
                {
                    realRoomSectorIds.Add(int.Parse(match.Groups[2].Value));
                }
            }
            return realRoomSectorIds.Sum();
        }

        public override object SolvePart2(string[] input)
        {
            var rooms = new Dictionary<string, int>();
            foreach (var line in input)
            {
                var match = RoomRegex().Match(line);
                var name = match.Groups[1].Value.Replace('-', ' ');
                var sectorId = int.Parse(match.Groups[2].Value);
                var shift = sectorId % 26;
                var room = string.Empty;
                foreach (var letter in name)
                {
                    if (letter == ' ')
                    {
                        room += letter;
                        continue;
                    }
                    room += (char)((letter - 'a' + shift) % 26 + 'a');
                }
                rooms.Add(room, sectorId);
            }
            return rooms.First(room => room.Key.Contains("north")).Value;
        }

        [GeneratedRegex(@"(.+)-(\d+)\[(.+)\]")]
        private static partial Regex RoomRegex();
    }
}
