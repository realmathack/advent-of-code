namespace AdventOfCode.Y2023.Solvers
{
    public class Day16 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid) => EnergizeTiles(grid, [new(new(0, 0), Coords.OffsetRight)]);

        public override object SolvePart2(char[][] grid)
        {
            var highest = 0;
            var beams = new List<Beam>();
            beams.AddRange(Enumerable.Range(0, grid.Length).Select(i => new Beam(new(0, i), Coords.OffsetRight)));
            beams.AddRange(Enumerable.Range(0, grid.Length).Select(i => new Beam(new(i, 0), Coords.OffsetDown)));
            beams.AddRange(Enumerable.Range(0, grid.Length).Select(i => new Beam(new(grid.Length - 1, i), Coords.OffsetLeft)));
            beams.AddRange(Enumerable.Range(0, grid.Length).Select(i => new Beam(new(i, grid[0].Length - 1), Coords.OffsetUp)));
            foreach (var beam in beams)
            {
                var tmp = EnergizeTiles(grid, [beam]);
                if (tmp > highest)
                {
                    highest = tmp;
                }
            }
            return highest;
        }

        private static int EnergizeTiles(char[][] grid, List<Beam> beams)
        {
            var visited = new HashSet<(Coords, Coords)>();
            while (beams.Count > 0)
            {
                for (int i = beams.Count - 1; i >= 0; i--)
                {
                    var beam = beams[i];
                    if (grid.IsOutOfBounds(beam.Pos) || !visited.Add((beam.Pos, beam.Direction)))
                    {
                        beams.RemoveAt(i);
                        continue;
                    }
                    var cell = grid[beam.Pos.Y][beam.Pos.X];
                    if (cell == '|' && (beam.Direction == Coords.OffsetLeft || beam.Direction == Coords.OffsetRight))
                    {
                        beams.Add(new(beam.Pos.Up, Coords.OffsetUp));
                        beams.Add(new(beam.Pos.Down, Coords.OffsetDown));
                        beams.RemoveAt(i);
                        continue;
                    }
                    else if (cell == '-' && (beam.Direction == Coords.OffsetUp || beam.Direction == Coords.OffsetDown))
                    {
                        beams.Add(new(beam.Pos.Left, Coords.OffsetLeft));
                        beams.Add(new(beam.Pos.Right, Coords.OffsetRight));
                        beams.RemoveAt(i);
                        continue;
                    }
                    else if (cell == '/')
                    {
                        beam.Direction = (beam.Direction == Coords.OffsetLeft || beam.Direction == Coords.OffsetRight) ? beam.Direction.RotateLeft : beam.Direction.RotateRight;
                    }
                    else if (cell == '\\')
                    {
                        beam.Direction = (beam.Direction == Coords.OffsetLeft || beam.Direction == Coords.OffsetRight) ? beam.Direction.RotateRight : beam.Direction.RotateLeft;
                    }
                    beam.Pos += beam.Direction;
                }
            }
            return visited.Select(tile => tile.Item1).Distinct().Count();
        }

        private class Beam(Coords pos, Coords direction)
        {
            public Coords Pos { get; set; } = pos;
            public Coords Direction { get; set; } = direction;
        }
    }
}
