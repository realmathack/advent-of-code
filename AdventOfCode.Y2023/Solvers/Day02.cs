namespace AdventOfCode.Y2023.Solvers
{
    public class Day02 : SolverWithLines
    {
        private const int _red = 0;
        private const int _green = 1;
        private const int _blue = 2;

        public override object SolvePart1(string[] input)
        {
            return ToGames(input)
                .Where(line => line.Sets.All(set => set[_red] <= 12 && line.Sets.All(set => set[_green] <= 13) && line.Sets.All(set => set[_blue] <= 14)))
                .Sum(game => game.Id);
        }

        public override object SolvePart2(string[] input) => ToGames(input).Sum(CalculateScore);

        private static List<Game> ToGames(string[] lines)
        {
            var games = new List<Game>();
            foreach (var line in lines)
            {
                var pos = line.IndexOf(':');
                var game = new Game(int.Parse(line[5..pos]));
                var sets = line[(pos + 2)..].Split("; ");
                foreach (var set in sets)
                {
                    var cubes = new int[] { 0, 0, 0 };
                    var parts = set.Split(", ");
                    foreach (var part in parts)
                    {
                        pos = part.IndexOf(' ');
                        var cube = part[(pos + 1)..] switch
                        {
                            "red" => _red,
                            "green" => _green,
                            "blue" => _blue,
                            _ => throw new InvalidOperationException($"Unknown color {part[(pos + 1)..]}")
                        };
                        cubes[cube] = int.Parse(part[..pos]);
                    }
                    game.Sets.Add(cubes);
                }
                games.Add(game);
            }
            return games;
        }

        private static int CalculateScore(Game game) => game.Sets.Max(set => set[_red]) * game.Sets.Max(set => set[_green]) * game.Sets.Max(set => set[_blue]);

        private record class Game(int Id)
        {
            public List<int[]> Sets { get; } = [[0, 0, 0]];
        }
    }
}
