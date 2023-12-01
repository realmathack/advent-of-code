namespace AdventOfCode.Y2021.Solvers
{
    public class Day05 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var lines = ToLines(input).Where(l => l.From.X == l.To.X || l.From.Y == l.To.Y).ToList();
            return FindOverlaps(lines);
        }

        public override object SolvePart2(string[] input)
        {
            return FindOverlaps(ToLines(input));
        }

        private static int FindOverlaps(List<Line> lines)
        {
            var overlaps = new HashSet<Coords>();
            var points = new HashSet<Coords>();
            foreach (var line in lines)
            {
                foreach (var point in ToCoords(line))
                {
                    if (!points.Add(point))
                    {
                        overlaps.Add(point);
                    }
                }
            }
            return overlaps.Count;
        }

        private static List<Line> ToLines(string[] input)
        {
            var lines = new List<Line>();
            foreach (var line in input)
            {
                var parts = line.Split(" -> ");
                lines.Add(new(ToCoords(parts[0]), ToCoords(parts[1])));
            }
            return lines;
        }

        private static Coords ToCoords(string input)
        {
            var parts = input.Split(',').Select(int.Parse).ToArray();
            return new(parts[0], parts[1]);
        }

        private static List<Coords> ToCoords(Line line)
        {
            var points = new List<Coords>() { line.From };
            var offset = line.From.OffsetTo(line.To);
            var current = line.From;
            while (current != line.To)
            {
                current += offset;
                points.Add(current);
            }
            return points;
        }

        private record class Line(Coords From, Coords To);
    }
}
