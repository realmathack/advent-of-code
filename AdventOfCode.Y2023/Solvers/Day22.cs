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
                var neighborsAbove = bricks[i].GetNeighborsAbove(bricks);
                if (neighborsAbove.Count == 0)
                {
                    count++;
                    continue;
                }
                if (neighborsAbove.All(neighbor => neighbor.GetNeighborsBelow(bricks).Count > 1))
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
                foreach (var neighborAbove in bricks[i].GetNeighborsAbove(bricks))
                {
                    queue.Enqueue(neighborAbove);
                }
                while (queue.TryDequeue(out var current))
                {
                    if (removed.Contains(current))
                    {
                        continue;
                    }
                    var shouldBeRemoved = current.GetNeighborsBelow(bricks).All(removed.Contains);
                    if (!shouldBeRemoved)
                    {
                        continue;
                    }
                    removed.Add(current);
                    foreach (var neighborAbove in current.GetNeighborsAbove(bricks))
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
            var isStillFalling = true;
            while (isStillFalling)
            {
                isStillFalling = false;
                bricks = [.. bricks.OrderBy(brick => Math.Min(brick.Start.Z, brick.End.Z)).ThenBy(brick => Math.Min(brick.Start.Y, brick.End.Y))];
                for (int i = 0; i < bricks.Count; i++)
                {
                    while (true)
                    {
                        var belowCoords = bricks[i].GetCoordsBelow();
                        if (belowCoords.Any(coord => coord.Z == 0))
                        {
                            break; // On the ground
                        }
                        if (bricks.Any(brick => brick.GetCubes().Any(cube => belowCoords.Contains(cube))))
                        {
                            break; // On another brick
                        }
                        isStillFalling = true;
                        bricks[i] = new Brick(bricks[i].Start.Down, bricks[i].End.Down);
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

            public HashSet<Coords3D> GetCoordsBelow()
            {
                var below = new HashSet<Coords3D>();
                var cubes = GetCubes();
                foreach (var cube in cubes)
                {
                    below.Add(cube.Down);
                }
                below.ExceptWith(cubes);
                return below;
            }

            public HashSet<Coords3D> GetCoordsAbove()
            {
                var above = new HashSet<Coords3D>();
                var cubes = GetCubes();
                foreach (var cube in cubes)
                {
                    above.Add(cube.Up);
                }
                above.ExceptWith(cubes);
                return above;
            }

            public HashSet<Brick> GetNeighborsBelow(List<Brick> bricks)
            {
                var coordsBelow = GetCoordsBelow();
                return bricks.Where(brick => coordsBelow.Any(coord => brick.GetCubes().Contains(coord))).ToHashSet();
            }

            public HashSet<Brick> GetNeighborsAbove(List<Brick> bricks)
            {
                var coordsAbove = GetCoordsAbove();
                return bricks.Where(brick => coordsAbove.Any(coord => brick.GetCubes().Contains(coord))).ToHashSet();
            }
        }
    }
}
