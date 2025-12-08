using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day18(int sideLength, int byteCount) : SolverWithLines
    {
        public Day18() : this(71, 1024) { }

        public override object SolvePart1(string[] input)
        {
            var corruptions = ToCorruptions(input, byteCount);
            return new AStar(sideLength, corruptions).FindShortestPath(new(0, 0), new(sideLength - 1, sideLength - 1)).Distance;
        }

        public override object SolvePart2(string[] input)
        {
            var corruptions = ToCorruptions(input);
            var start = new Coords(0, 0);
            var goal = new Coords(sideLength - 1, sideLength - 1);
            var activeCorruptions = new HashSet<Coords>(corruptions[0..byteCount]);
            var path = new HashSet<Coords>(new AStar(sideLength, activeCorruptions).FindShortestPath(start, goal).Nodes);
            for (int i = byteCount; i < corruptions.Length; i++)
            {
                var corruption = corruptions[i];
                activeCorruptions.Add(corruption);
                if (path.Contains(corruption))
                {
                    var shortestPath = new AStar(sideLength, activeCorruptions).FindShortestPath(start, goal);
                    if (shortestPath == Graphs.GraphPath<Coords>.Empty)
                    {
                        return corruption.ToString();
                    }
                    path = new(shortestPath.Nodes);
                }
            }
            return "Not found";
        }

        private static List<Coords> FindPossibleNeighbors(int sideLength, HashSet<Coords> corruptions, Coords current)
        {
            var neighbors = new List<Coords>();
            if (current.X > 0              && !corruptions.Contains(current.Left) ) { neighbors.Add(current.Left); }
            if (current.Y > 0              && !corruptions.Contains(current.Up)   ) { neighbors.Add(current.Up); }
            if (current.X < sideLength - 1 && !corruptions.Contains(current.Right)) { neighbors.Add(current.Right); }
            if (current.Y < sideLength - 1 && !corruptions.Contains(current.Down) ) { neighbors.Add(current.Down); }
            return neighbors;
        }

        private static HashSet<Coords> ToCorruptions(string[] lines, int maxCount)
        {
            var count = Math.Min(lines.Length, maxCount);
            var corruptions = new HashSet<Coords>(count);
            for (int i = 0; i < count; i++)
            {
                corruptions.Add(Coords.Parse(lines[i]));
            }
            return corruptions;
        }

        private static Coords[] ToCorruptions(string[] lines)
        {
            var corruptions = new Coords[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                corruptions[i] = Coords.Parse(lines[i]);
            }
            return corruptions;
        }

        public bool IsOutOfBounds(Coords coords) => coords.Y < 0 || coords.X < 0 || coords.Y >= sideLength || coords.X >= sideLength;

        private class AStar(int sideLength, HashSet<Coords> corruptions) : Graphs.AStar<Coords>
        {
            protected override int CalculateHeuristic(Coords node, Coords goal) => node.DistanceTo(goal);
            protected override List<Coords> FindNeighbors(Coords node) => FindPossibleNeighbors(sideLength, corruptions, node);
        }
    }
}
