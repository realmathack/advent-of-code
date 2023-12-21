namespace AdventOfCode.Y2018.Solvers
{
    public class Day06(int _limit) : SolverWithLines
    {
        public Day06() : this(10000) { }

        public override object SolvePart1(string[] input)
        {
            var points = ToPoints(input);
            var bottomRight = new Coords(points.Max(point => point.Coords.X) * 110 / 100, points.Max(point => point.Coords.Y) * 110 / 100);
            var counts = new Dictionary<char, int>();
            var infiniteIds = new HashSet<char>();
            for (int y = 0; y <= bottomRight.Y; y++)
            {
                for (int x = 0; x <= bottomRight.X; x++)
                {
                    var current = new Coords(x, y);
                    char? nearestId = null;
                    var nearestDistance = int.MaxValue;
                    foreach (var point in points)
                    {
                        var tmp = current.DistanceTo(point.Coords);
                        if (tmp < nearestDistance)
                        {
                            nearestId = point.Id;
                            nearestDistance = tmp;
                        }
                        else if (tmp == nearestDistance)
                        {
                            nearestId = null;
                        }
                    }
                    if (nearestId.HasValue)
                    {
                        if (x == 0 || y == 0 || x == bottomRight.X || y == bottomRight.Y)
                        {
                            infiniteIds.Add(nearestId.Value);
                            continue;
                        }
                        if (!counts.TryGetValue(nearestId.Value, out int count))
                        {
                            count = 0;
                        }
                        counts[nearestId.Value] = ++count;
                    }
                }
            }
            foreach (var infiniteId in infiniteIds)
            {
                counts.Remove(infiniteId);
            }
            return counts.Max(count => count.Value);
        }

        public override object SolvePart2(string[] input)
        {
            var points = ToPoints(input);
            var bottomRight = new Coords(points.Max(point => point.Coords.X) * 110 / 100, points.Max(point => point.Coords.Y) * 110 / 100);
            var count = 0;
            for (int y = 0; y <= bottomRight.Y; y++)
            {
                for (int x = 0; x <= bottomRight.X; x++)
                {
                    var current = new Coords(x, y);
                    var tmp = points.Sum(point => current.DistanceTo(point.Coords));
                    if (tmp < _limit)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static List<(Coords Coords, char Id)> ToPoints(string[] lines)
        {
            var points = new List<(Coords Coords, char Id)>();
            foreach (var line in lines)
            {
                var parts = line.Split(", ");
                points.Add((new(int.Parse(parts[0]), int.Parse(parts[1])), (char)('A' + points.Count)));
            }
            return points;
        }
    }
}
