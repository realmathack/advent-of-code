using Coords3D = AdventOfCode.Coords3D<int>;

namespace AdventOfCode.Y2025.Solvers
{
    public class Day08(int connections) : SolverWithLines
    {
        public Day08() : this(1000) { }

        public override object SolvePart1(string[] input)
        {
            var boxes = input.Select(Coords3D.Parse).ToArray();
            var distances = CalculateDistances(boxes);
            var groups = new List<HashSet<Coords3D>>();
            foreach (var (a, b, _) in distances.Take(connections))
            {
                ConnectBoxes(a, b, groups);
            }
            var product = groups.Select(group => group.Count).OrderDescending().Take(3).Product();
            return product;
        }

        public override object SolvePart2(string[] input)
        {
            var boxes = input.Select(Coords3D.Parse).ToArray();
            var distances = CalculateDistances(boxes);
            var groups = new List<HashSet<Coords3D>>();
            foreach (var (a, b, _) in distances)
            {
                ConnectBoxes(a, b, groups);
                if (groups.Count == 1 && groups[0].Count == boxes.Length)
                {
                    return (long)a.X * b.X;
                }
            }
            throw new SolutionNotFoundException();
        }

        private static void ConnectBoxes(Coords3D a, Coords3D b, List<HashSet<Coords3D>> groups)
        {
            var groupA = groups.FirstOrDefault(group => group.Contains(a));
            var groupB = groups.FirstOrDefault(group => group.Contains(b));
            if (groupA is null)
            {
                if (groupB is null)
                {
                    groups.Add([a, b]);
                }
                else
                {
                    groupB.Add(a);
                }
            }
            else
            {
                if (groupB is null)
                {
                    groupA.Add(b);
                }
                else if (groupA != groupB)
                {
                    groupA.UnionWith(groupB);
                    groups.Remove(groupB);
                }
            }
        }

        private static IEnumerable<(Coords3D<int> A, Coords3D<int> B, double Distance)> CalculateDistances(Coords3D<int>[] boxes)
        {
            var distances = new List<(Coords3D A, Coords3D B, double Distance)>();
            for (int a = 0; a < boxes.Length; a++)
            {
                for (int b = a + 1; b < boxes.Length; b++)
                {
                    distances.Add((boxes[a], boxes[b], CalculateDistance(boxes[a], boxes[b])));
                }
            }
            return distances.OrderBy(distance => distance.Distance);
        }

        private static double CalculateDistance(Coords3D a, Coords3D b)
        {
            long x = a.X - b.X;
            long y = a.Y - b.Y;
            long z = a.Z - b.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }
}
