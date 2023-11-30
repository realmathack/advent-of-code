namespace AdventOfCode.Y2018.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return ToClaimedSquares(input).Count(x => x.Value.Count >= 2);
        }

        public override object SolvePart2(string[] input)
        {
            var claimedSquares = ToClaimedSquares(input);
            var singles = claimedSquares.Where(x => x.Value.Count == 1).ToList();
            var singleIds = singles.Select(x => x.Value.Single()).Distinct().ToList();
            var others = claimedSquares.Except(singles).ToList();
            var otherIds = others.SelectMany(x => x.Value).Distinct().ToList();
            foreach (var id in singleIds)
            {
                if (!otherIds.Contains(id))
                {
                    return id;
                }
            }
            return 0;
        }

        private Dictionary<Coords, List<int>> ToClaimedSquares(string[] input)
        {
            var claimedSquares = new Dictionary<Coords, List<int>>();
            foreach (var claim in ToClaims(input))
            {
                foreach (var square in ToSquares(claim))
                {
                    if (!claimedSquares.TryGetValue(square, out var counts))
                    {
                        counts = [];
                    }
                    claimedSquares[square] = [.. counts, claim.Id];
                }
            }
            return claimedSquares;
        }

        private static readonly char[] _separator = [' ', '#', '@', ',', ':', 'x'];
        private static List<Claim> ToClaims(string[] input)
        {
            var claims = new List<Claim>();
            foreach (var line in input)
            {
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                claims.Add(new(parts[0], new(parts[1], parts[2]), parts[3], parts[4]));
            }
            return claims;
        }

        private static List<Coords> ToSquares(Claim claim)
        {
            var squares = new List<Coords>();
            for (int x = 0; x < claim.Width; x++)
            {
                for (int y = 0; y < claim.Height; y++)
                {
                    squares.Add(claim.Coords + (x, y));
                }
            }
            return squares;
        }

        private record class Claim(int Id, Coords Coords, int Width, int Height);
    }
}
