using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2019.Solvers
{
    public class Day10 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid)
        {
            var asteroids = ToAsteroids(grid);
            return asteroids.Max(asteriod => CalculateVisible(asteriod, asteroids));
        }

        public override object SolvePart2(char[][] grid)
        {
            var asteroids = ToAsteroids(grid);
            var station = asteroids.OrderByDescending(asteroid => CalculateVisible(asteroid, asteroids)).First();
            var angles = new SortedDictionary<double, SortedList<int, Coords>>();
            foreach (var asteroid in asteroids)
            {
                double dx = asteroid.X - station.X;
                double dy = asteroid.Y - station.Y;
                // Angle in radians, (Math.PI/2) is to make upwards 0 radians, (Math.PI * 2) addition/modulo is to move negative numbers to the back of the list
                var angle = (Math.PI/2 + Math.Atan2(dy, dx) + Math.PI * 2) % (Math.PI * 2);
                if (!angles.TryGetValue(angle, out var line))
                {
                    line = [];
                    angles.Add(angle, line);
                }
                line.Add(station.DistanceTo(asteroid), asteroid);
            }
            var vaporizations = 0;
            while (vaporizations < 200)
            {
                foreach (var angle in angles.Keys)
                {
                    if (angles[angle].Count == 0)
                    {
                        continue;
                    }
                    var asteroid = angles[angle].GetValueAtIndex(0);
                    angles[angle].RemoveAt(0);
                    if (++vaporizations == 200)
                    {
                        return asteroid.X * 100 + asteroid.Y;
                    }
                }
            }
            return 0;

            // TODO:
            // https://github.com/gercobrandwijk/AdventOfCode/blob/master/AdventOfCode2019/Day10.cs
            // other solutions:
            // https://github.com/jeroenheijmans/advent-of-code-2019/blob/master/solutions/day10/part1.py
            // https://github.com/genveir/Advent/blob/master/Advent2019/Advent10/Solution.cs
            // https://github.com/maksverver/AdventOfCode/blob/master/2019/10A.py
        }

        private static int CalculateVisible(Coords current, List<Coords> asteroids)
        {
            var visibleAngles = new HashSet<double>();
            foreach (var asteroid in asteroids)
            {
                if (asteroid == current)
                {
                    continue;
                }
                double dx = asteroid.X - current.X;
                double dy = asteroid.Y - current.Y;
                var angle = Math.Atan2(dy, dx); // angle in radians (see MS-Learn example)
                visibleAngles.Add(angle);
            }
            return visibleAngles.Count;
        }

        private static List<Coords> ToAsteroids(char[][] grid)
        {
            var asteroids = new List<Coords>();
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (grid[y][x] == '#')
                    {
                        asteroids.Add(new(x, y));
                    }
                }
            }
            return asteroids;
        }
    }
}
