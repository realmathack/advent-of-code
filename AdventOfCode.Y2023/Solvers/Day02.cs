namespace AdventOfCode.Y2023.Solvers
{
    public class Day02 : SolverWithLines
    {
        private const int _redIndex = 0;
        private const int _greenIndex = 1;
        private const int _blueIndex = 2;

        public override object SolvePart1(string[] input)
        {
            return ToGames(input)
                .Where(x => x.Sets.All(s => s[_redIndex] <= 12 && x.Sets.All(s => s[_greenIndex] <= 13) && x.Sets.All(s => s[_blueIndex] <= 14)))
                .Sum(x => x.Id);
        }

        public override object SolvePart2(string[] input)
        {
            return ToGames(input).Sum(CalculateScore);
        }

        private static int CalculateScore(Game game) => game.Sets.Max(x => x[_redIndex]) * game.Sets.Max(x => x[_greenIndex]) * game.Sets.Max(x => x[_blueIndex]);

        private static List<Game> ToGames(string[] input)
        {
            var games = new List<Game>();
            foreach (var line in input)
            {
                var parts = line.Split(": ");
                var game = new Game(int.Parse(parts[0][5..]));
                var sets = parts[1].Split("; ");
                foreach (var set in sets)
                {
                    var cubes = new int[] { 0, 0, 0 };
                    parts = set.Split(", ");
                    foreach (var part in parts)
                    {
                        var pos = part.IndexOf(' ');
                        var cube = part[(pos + 1)..] switch
                        {
                            "red" => _redIndex,
                            "green" => _greenIndex,
                            "blue" => _blueIndex,
                            _ => throw new InvalidOperationException("Unknown color")
                        };
                        cubes[cube] = int.Parse(part[..pos]);
                    }
                    game.Sets.Add(cubes);
                }
                games.Add(game);
            }
            return games;
        }

        private record class Game(int Id)
        {
            public List<int[]> Sets { get; } = [[0, 0, 0]];
        }
    }
}
