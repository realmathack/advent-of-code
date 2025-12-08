using Coords3D = AdventOfCode.Coords3D<int>;

namespace AdventOfCode.Y2022.Solvers
{
    public class Day18 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var cubes = ToCubes(input);
            var sum = 0;
            foreach (var cube in cubes)
            {
                foreach (var neighbor in cube.Neighbors)
                {
                    if (!cubes.Contains(neighbor))
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        public override object SolvePart2(string[] input)
        {
            var cubes = ToCubes(input);
            var visited = new HashSet<Coords3D>();
            var sum = 0;
            var minPoint = new Coords3D(cubes.Min(cube => cube.X) - 1, cubes.Min(cube => cube.Y) - 1, cubes.Min(cube => cube.Z) - 1);
            var maxPoint = new Coords3D(cubes.Max(cube => cube.X) + 1, cubes.Max(cube => cube.Y) + 1, cubes.Max(cube => cube.Z) + 1);
            var queue = new Queue<Coords3D>();
            queue.Enqueue(minPoint);
            while (queue.TryDequeue(out var current))
            {
                if (!visited.Add(current))
                {
                    continue;
                }
                foreach (var neighbor in current.Neighbors)
                {
                    if (neighbor.X < minPoint.X || neighbor.Y < minPoint.Y || neighbor.Z < minPoint.Z ||
                        neighbor.X > maxPoint.X || neighbor.Y > maxPoint.Y || neighbor.Z > maxPoint.Z || visited.Contains(neighbor))
                    {
                        continue;
                    }
                    if (cubes.Contains(neighbor))
                    {
                        sum++;
                    }
                    else
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return sum;
        }

        private static HashSet<Coords3D> ToCubes(string[] lines)
        {
            var cubes = new HashSet<Coords3D>();
            foreach (var line in lines)
            {
                cubes.Add(Coords3D.Parse(line));
            }
            return cubes;
        }
    }
}
