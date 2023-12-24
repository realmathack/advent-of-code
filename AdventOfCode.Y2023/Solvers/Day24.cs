namespace AdventOfCode.Y2023.Solvers
{
    public class Day24(double _testLow, double _testHigh) : SolverWithLines
    {
        public Day24() : this(200000000000000, 400000000000000) { }

        public override object SolvePart1(string[] input)
        {
            var hailstones = ToHailstones(input);
            var count = 0;
            for (int i = 0; i < hailstones.Count - 1; i++)
            {
                for (int j = i + 1; j < hailstones.Count; j++)
                {
                    // https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection
                    var x1 = hailstones[i].Px;
                    var y1 = hailstones[i].Py;
                    var x2 = hailstones[i].Px + hailstones[i].Vx;
                    var y2 = hailstones[i].Py + hailstones[i].Vy;
                    var x3 = hailstones[j].Px;
                    var y3 = hailstones[j].Py;
                    var x4 = hailstones[j].Px + hailstones[j].Vx;
                    var y4 = hailstones[j].Py + hailstones[j].Vy;
                    var denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
                    if (denominator == 0D)
                    {
                        continue;
                    }
                    var t1 = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
                    var t2 = ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) / denominator;
                    if (t1 < 0D || t2 < 0D)
                    {
                        continue;
                    }
                    var x = x1 + t1 * hailstones[i].Vx;
                    var y = y1 + t1 * hailstones[i].Vy;
                    if (x >= _testLow && x <= _testHigh && y >= _testLow && y <= _testHigh)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Implement
            return null!;
        }

        private static List<Hailstone> ToHailstones(string[] lines)
        {
            var hailstones = new List<Hailstone>();
            foreach (var line in lines)
            {
                var (position, velocity) = line.SplitInTwo(" @ ");
                var pos = position.Split(", ").Select(double.Parse).ToArray();
                var velo = velocity.Split(", ").Select(int.Parse).ToArray();
                hailstones.Add(new(pos[0], pos[1], pos[2], velo[0], velo[1], velo[2]));
            }
            return hailstones;
        }

        private record class Hailstone(double Px, double Py, double Pz, int Vx, int Vy, int Vz);
    }
}
