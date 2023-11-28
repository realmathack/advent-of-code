namespace AdventOfCode.Y2022.Solvers
{
    public class Day14 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (formations, bottomRight, sandStart) = ToFormations(input);
            return SimulateSand(formations, bottomRight, sandStart);
        }

        public override object SolvePart2(string[] input)
        {
            var (formations, bottomRight, sandStart) = ToFormations(input, true);
            return SimulateSand(formations, bottomRight, sandStart);
        }

        private static int SimulateSand(List<Coords[]> formations, Coords bottomRight, Coords sandStart)
        {
            var grid = ToGrid(bottomRight);
            AddFormationsToGrid(formations, grid);
            var sandDropped = 0;
            var sand = sandStart;
            while (true)
            {
                if (sand.Down.Y > bottomRight.Y)
                {
                    break; // Abyss
                }
                if (!grid[sand.Down.Y][sand.Down.X])
                {
                    sand = sand.Down;
                    continue;
                }
                if (sand.DownLeft.X < 0)
                {
                    break; // Abyss
                }
                if (!grid[sand.DownLeft.Y][sand.DownLeft.X])
                {
                    sand = sand.DownLeft;
                    continue;
                }
                if (sand.DownRight.X > bottomRight.X)
                {
                    break; // Abyss
                }
                if (!grid[sand.DownRight.Y][sand.DownRight.X])
                {
                    sand = sand.DownRight;
                    continue;
                }
                grid[sand.Y][sand.X] = true;
                sandDropped++;
                if (sand == sandStart)
                {
                    break; // Space filled
                }
                sand = sandStart;
            }
            return sandDropped;
        }

        private static void AddFormationsToGrid(List<Coords[]> formations, bool[][] grid)
        {
            foreach (var formation in formations)
            {
                var current = formation[0];
                grid[current.Y][current.X] = true;
                for (int i = 1; i < formation.Length; i++)
                {
                    var offset = current.OffsetTo(formation[i]);
                    var distance = current.DistanceTo(formation[i]);
                    for (int count = 0; count < distance; count++)
                    {
                        current += offset;
                        grid[current.Y][current.X] = true;
                    }
                    current = formation[i];
                }
            }
        }

        private static (List<Coords[]> formations, Coords bottomRight, Coords sandStart) ToFormations(string[] lines, bool p2 = false)
        {
            var sandStart = new Coords(500, 0);
            var topLeft = sandStart;
            var bottomRight = sandStart;
            var formations = new List<Coords[]>();
            foreach (var line in lines)
            {
                var formation = line.Split(" -> ").Select(point => point.Split(',').Select(int.Parse).ToArray()).Select(coord => new Coords(coord[0], coord[1])).ToArray();
                var minX = formation.Min(coords => coords.X);
                if (minX < topLeft.X)
                {
                    topLeft = new(minX, topLeft.Y);
                }
                var maxX = formation.Max(coords => coords.X);
                if (maxX > bottomRight.X)
                {
                    bottomRight = new(maxX, bottomRight.Y);
                }
                var maxY = formation.Max(coords => coords.Y);
                if (maxY > bottomRight.Y)
                {
                    bottomRight = new(bottomRight.X, maxY);
                }
                formations.Add(formation);
            }
            if (p2)
            {
                topLeft -= new Coords(bottomRight.Y, 0);
                bottomRight += new Coords(bottomRight.Y, 2);
                formations.Add([new(topLeft.X, bottomRight.Y), bottomRight]);
            }
            for (int i = 0; i < formations.Count; i++)
            {
                formations[i] = formations[i].Select(coords => coords - topLeft).ToArray();
            }
            sandStart -= topLeft;
            bottomRight -= topLeft;
            return (formations, bottomRight, sandStart);
        }

        private static bool[][] ToGrid(Coords bottomRight)
        {
            var grid = new bool[bottomRight.Y + 1][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new bool[bottomRight.X + 1];
            }
            return grid;
        }
    }
}
