namespace AdventOfCode.Y2023.Solvers
{
    public class Day24(double _testLow, double _testHigh) : SolverWithLines
    {
        public Day24() : this(200_000_000_000_000, 400_000_000_000_000) { }

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
                    if (denominator == 0M)
                    {
                        continue;
                    }
                    var t1 = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / (double)denominator;
                    var t2 = ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) / (double)denominator;
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

        // HACK: Implemented https://old.reddit.com/r/adventofcode/comments/18pnycy/2023_day_24_solutions/keqf8uq/
        // This only works for the real input, not for the test input
        public override object SolvePart2(string[] input)
        {
            var hailstones = ToHailstones(input);
            HashSet<long>? potentialXs = null;
            HashSet<long>? potentialYs = null;
            HashSet<long>? potentialZs = null;
            for (int i = 0; i < hailstones.Count - 1; i++)
            {
                for (int j = i + 1; j < hailstones.Count; j++)
                {
                    if (hailstones[i].Vx == hailstones[j].Vx)
                    {
                        var newXs = new HashSet<long>();
                        var difference = hailstones[j].Px - hailstones[i].Px;
                        for (int v = -1000; v < 1000; v++)
                        {
                            if (v == hailstones[i].Vx)
                            {
                                continue;
                            }
                            if (difference % (v - hailstones[i].Vx) == 0)
                            {
                                newXs.Add(v);
                            }
                        }
                        if (potentialXs is null)
                        {
                            potentialXs = newXs;
                        }
                        else
                        {
                            potentialXs.IntersectWith(newXs);
                        }
                    }
                }
            }
            for (int i = 0; i < hailstones.Count - 1; i++)
            {
                for (int j = i + 1; j < hailstones.Count; j++)
                {
                    if (hailstones[i].Vy == hailstones[j].Vy)
                    {
                        var newYs = new HashSet<long>();
                        var difference = hailstones[j].Py - hailstones[i].Py;
                        for (int v = -1000; v < 1000; v++)
                        {
                            if (v == hailstones[i].Vy)
                            {
                                continue;
                            }
                            if (difference % (v - hailstones[i].Vy) == 0)
                            {
                                newYs.Add(v);
                            }
                        }
                        if (potentialYs is null)
                        {
                            potentialYs = newYs;
                        }
                        else
                        {
                            potentialYs.IntersectWith(newYs);
                        }
                    }
                }
            }
            for (int i = 0; i < hailstones.Count - 1; i++)
            {
                for (int j = i + 1; j < hailstones.Count; j++)
                {
                    if (hailstones[i].Vz == hailstones[j].Vz)
                    {
                        var newZs = new HashSet<long>();
                        var difference = hailstones[j].Pz - hailstones[i].Pz;
                        for (int v = -1000; v < 1000; v++)
                        {
                            if (v == hailstones[i].Vz)
                            {
                                continue;
                            }
                            if (difference % (v - hailstones[i].Vz) == 0)
                            {
                                newZs.Add(v);
                            }
                        }
                        if (potentialZs is null)
                        {
                            potentialZs = newZs;
                        }
                        else
                        {
                            potentialZs.IntersectWith(newZs);
                        }
                    }
                }
            }
            var rvx = potentialXs?.First() ?? 0L;
            var rvy = potentialYs?.First() ?? 0L;
            var rvz = potentialZs?.First() ?? 0L;
            var ma = (hailstones[0].Vy - rvy) / (double)(hailstones[0].Vx - rvx);
            var mb = (hailstones[1].Vy - rvy) / (double)(hailstones[1].Vx - rvx);
            var ca = hailstones[0].Py - ma * hailstones[0].Px;
            var cb = hailstones[1].Py - mb * hailstones[1].Px;
            var x = Math.Round((cb - ca) / (ma - mb));
            var y = Math.Round(ma * x + ca);
            var t = Math.Floor((x - hailstones[0].Px) / (hailstones[0].Vx - rvx));
            var z = hailstones[0].Pz + (hailstones[0].Vz - rvz) * t;
            return x + y + z;
        }

        private static List<Hailstone> ToHailstones(string[] lines)
        {
            var hailstones = new List<Hailstone>();
            foreach (var line in lines)
            {
                var (position, velocity) = line.SplitInTwo(" @ ");
                var pos = position.Split(", ").Select(long.Parse).ToArray();
                var velo = velocity.Split(", ").Select(long.Parse).ToArray();
                hailstones.Add(new(pos[0], pos[1], pos[2], velo[0], velo[1], velo[2]));
            }
            return hailstones;
        }

        private record class Hailstone(long Px, long Py, long Pz, long Vx, long Vy, long Vz);
    }
}
