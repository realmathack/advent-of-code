namespace AdventOfCode.Y2023.Solvers
{
    public class Day22 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var bricks = ToBricks(input);
            bricks = SimulateFalling(bricks);
            var count = 0;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Above.Count == 0 || bricks[i].Above.All(neighbor => neighbor.Below.Count > 1))
                {
                    count++;
                }
            }
            return count;
        }

        public override object SolvePart2(string[] input)
        {
            var bricks = ToBricks(input);
            bricks = SimulateFalling(bricks);
            var sum = 0;
            for (int i = 0; i < bricks.Count; i++)
            {
                var removed = new HashSet<Brick>() { bricks[i] };
                var queue = new Queue<Brick>();
                foreach (var neighborAbove in bricks[i].Above)
                {
                    queue.Enqueue(neighborAbove);
                }
                while (queue.TryDequeue(out var current))
                {
                    if (removed.Contains(current) || !current.Below.All(removed.Contains))
                    {
                        continue;
                    }
                    removed.Add(current);
                    foreach (var neighborAbove in current.Above)
                    {
                        queue.Enqueue(neighborAbove);
                    }
                }
                sum += removed.Count - 1;
            }
            return sum;
        }

        private static List<Brick> SimulateFalling(List<Brick> bricks)
        {
            var grid = new Dictionary<Coords, Dictionary<int, Brick>>();
            foreach (var brick in bricks)
            {
                foreach (var cube in brick.GetCubes())
                {
                    if (!grid.TryGetValue(new(cube.X, cube.Y), out var zs))
                    {
                        zs = [];
                        grid[new(cube.X, cube.Y)] = zs;
                    }
                    zs.Add(cube.Z, brick);
                }
            }
            bricks = [.. bricks.OrderBy(brick => brick.Start.Z)];
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Start.Z == 1)
                {
                    continue;
                }
                var cubes = bricks[i].GetCubes();
                var newZ = 1;
                foreach (var cube in cubes)
                {
                    var zs = grid[new(cube.X, cube.Y)];
                    var below = zs.Where(z => z.Key < cube.Z).Select(z => z.Key).ToArray();
                    var tmp = below.Length == 0 ? 1 : below.Max() + 1;
                    if (tmp > newZ)
                    {
                        newZ = tmp;
                    }
                    zs.Remove(cube.Z);
                }
                var offset = new Coords3D(0, 0, newZ - bricks[i].Start.Z);
                bricks[i] = new(bricks[i].Start + offset, bricks[i].End + offset);
                foreach (var cube in bricks[i].GetCubes())
                {
                    var zs = grid[new(cube.X, cube.Y)];
                    zs.Add(cube.Z, bricks[i]);
                    if (zs.TryGetValue(cube.Z - 1, out var brickBelow) && brickBelow != bricks[i])
                    {
                        bricks[i].Below.Add(brickBelow);
                        brickBelow.Above.Add(bricks[i]);
                    }
                }
            }
            return bricks;
        }

        private static List<Brick> ToBricks(string[] lines)
        {
            var bricks = new List<Brick>();
            foreach (var line in lines)
            {
                var parts = line.Split(',', '~').Select(int.Parse).ToArray();
                bricks.Add(new(new(parts[0], parts[1], parts[2]), new(parts[3], parts[4], parts[5])));
            }
            return bricks;
        }

        private record class Brick(Coords3D Start, Coords3D End)
        {
            public HashSet<Brick> Above { get; } = [];
            public HashSet<Brick> Below { get; } = [];

            private HashSet<Coords3D>? _cubes;
            public HashSet<Coords3D> GetCubes()
            {
                if (_cubes is not null)
                {
                    return _cubes;
                }
                _cubes = [];
                if (Start.X != End.X)
                {
                    for (int x = Start.X; x <= End.X; x++)
                    {
                        _cubes.Add(new(x, Start.Y, Start.Z));
                    }
                }
                else if (Start.Y != End.Y)
                {
                    for (int y = Start.Y; y <= End.Y; y++)
                    {
                        _cubes.Add(new(Start.X, y, Start.Z));
                    }
                }
                else
                {
                    for (int z = Start.Z; z <= End.Z; z++)
                    {
                        _cubes.Add(new(Start.X, Start.Y, z));
                    }
                }
                return _cubes;
            }
        }
    }
}
