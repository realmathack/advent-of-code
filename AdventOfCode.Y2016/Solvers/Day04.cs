namespace AdventOfCode.Y2016.Solvers
{
    public class Day04 : SolverWithLines
    {
        private static readonly char[] _separator = ['-', '[', ']'];

        public override object SolvePart1(string[] input)
        {
            var realRoomSectorIds = new List<int>();
            foreach (var line in input)
            {
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                var name = string.Concat(parts[..^2]);
                var top5 = name.GroupBy(c => c).Select(g => (g.Key, Count: g.Count())).OrderByDescending(g => g.Count).ThenBy(g => g.Key).Take(5).Select(g => g.Key).ToHashSet();
                if (parts[^1].All(top5.Contains))
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
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                var name = string.Join(' ', parts[..^2]);
                var sectorId = int.Parse(parts[^2]);
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
    }
}
