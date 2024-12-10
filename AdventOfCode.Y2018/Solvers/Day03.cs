using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2018.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToClaimedSquares(input).Values.Count(square => square.Ids.Count >= 2);

        public override object SolvePart2(string[] input)
        {
            var claimedSquares = ToClaimedSquares(input).Values;
            var singles = claimedSquares.Where(square => square.Ids.Count == 1).ToArray();
            var singleIds = singles.Select(single => single.Ids.Single()).Distinct().ToArray();
            var others = claimedSquares.Except(singles).ToArray();
            var otherIds = others.SelectMany(other => other.Ids).ToHashSet();
            foreach (var id in singleIds)
            {
                if (!otherIds.Contains(id))
                {
                    return id;
                }
            }
            return 0;
        }

        private static Dictionary<Coords, ClaimedSquare> ToClaimedSquares(string[] lines)
        {
            var claimedSquares = new Dictionary<Coords, ClaimedSquare>();
            foreach (var claim in ToClaims(lines))
            {
                foreach (var square in ToSquares(claim))
                {
                    if (!claimedSquares.TryGetValue(square, out var claimedSquare))
                    {
                        claimedSquare = new(square, []);
                        claimedSquares[square] = claimedSquare;
                    }
                    claimedSquare.Ids.Add(claim.Id);
                }
            }
            return claimedSquares;
        }

        private static readonly char[] _separator = [' ', '#', '@', ',', ':', 'x'];
        private static List<Claim> ToClaims(string[] lines)
        {
            var claims = new List<Claim>();
            foreach (var line in lines)
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
                    squares.Add(claim.Position + (x, y));
                }
            }
            return squares;
        }

        private record class Claim(int Id, Coords Position, int Width, int Height);
        private record class ClaimedSquare(Coords Position, HashSet<int> Ids);
    }
}
