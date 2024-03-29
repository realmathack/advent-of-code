namespace AdventOfCode.Y2021.Solvers
{
    public class Day05 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => FindOverlaps(ToVents(input).Where(vent => vent.From.X == vent.To.X || vent.From.Y == vent.To.Y).ToList());
        public override object SolvePart2(string[] input) => FindOverlaps(ToVents(input));

        private static int FindOverlaps(List<Vent> vents)
        {
            var overlaps = new HashSet<Coords>();
            var points = new HashSet<Coords>();
            foreach (var vent in vents)
            {
                foreach (var point in ToCoords(vent))
                {
                    if (!points.Add(point))
                    {
                        overlaps.Add(point);
                    }
                }
            }
            return overlaps.Count;
        }

        private static List<Vent> ToVents(string[] lines)
        {
            var vents = new List<Vent>();
            foreach (var line in lines)
            {
                var (from, to) = line.SplitInTwo(" -> ");
                vents.Add(new(ToCoords(from), ToCoords(to)));
            }
            return vents;
        }

        private static Coords ToCoords(string coords)
        {
            var parts = coords.Split(',').Select(int.Parse).ToArray();
            return new(parts[0], parts[1]);
        }

        private static List<Coords> ToCoords(Vent vent)
        {
            var points = new List<Coords>() { vent.From };
            var offset = vent.From.OffsetTo(vent.To);
            var current = vent.From;
            while (current != vent.To)
            {
                current += offset;
                points.Add(current);
            }
            return points;
        }

        private record class Vent(Coords From, Coords To);
    }
}
