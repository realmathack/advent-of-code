using Coords = AdventOfCode.Coords<int>;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Solvers
{
    public partial class Day14(int _width, int _height) : SolverWithLines
    {
        public Day14() : this(101, 103) { }

        public override object SolvePart1(string[] input)
        {
            var robots = ToRobots(input);
            for (int i = 0; i < 100; i++)
            {
                MoveRobots(robots);
            }
            return CalculateSafetyFactor(robots);
        }

        public override object SolvePart2(string[] input)
        {
            var robots = ToRobots(input);
            for (int i = 0; i < _width * _height; i++)
            {
                if (RobotsClustered(robots))
                {
                    var image = GenerateImage(robots);
                    return $"{i}{Environment.NewLine}{image}";
                }
                MoveRobots(robots);
            }
            return -1;
        }

        private void MoveRobots(List<Robot> robots)
        {
            foreach (var robot in robots)
            {
                var x = (_width + robot.Position.X + robot.Velocity.X) % _width;
                var y = (_height + robot.Position.Y + robot.Velocity.Y) % _height;
                robot.Position = new(x, y);
            }
        }

        private int CalculateSafetyFactor(List<Robot> robots)
        {
            var centerX = _width / 2;
            var centerY = _height / 2;
            return robots.Count(robot => robot.Position.X < centerX && robot.Position.Y < centerY) *
                robots.Count(robot => robot.Position.X > centerX && robot.Position.Y < centerY) *
                robots.Count(robot => robot.Position.X < centerX && robot.Position.Y > centerY) *
                robots.Count(robot => robot.Position.X > centerX && robot.Position.Y > centerY);
        }

        private static bool RobotsClustered(List<Robot> robots)
        {
            var positions = new HashSet<Coords>(robots.Select(robot => robot.Position));
            var multiNeighborCount = 0;
            foreach (var robot in robots)
            {
                if (robot.Position.Neighbors.Count(positions.Contains) > 1)
                {
                    multiNeighborCount++;
                }
            }
            return multiNeighborCount > (robots.Count / 5);
        }

        private string GenerateImage(List<Robot> robots)
        {
            var grid = new bool[_height][];
            for (int y = 0; y < grid.Length; y++)
            {
                grid[y] = new bool[_width];
            }
            foreach (var robot in robots)
            {
                grid[robot.Position.Y][robot.Position.X] = true;
            }
            return string.Concat(grid.Select(row => Environment.NewLine + string.Concat(row.Select(value => value ? '█' : ' '))));
        }

        private static List<Robot> ToRobots(string[] lines)
        {
            var robots = new List<Robot>();
            foreach (var line in lines)
            {
                var match = RobotRegex().Match(line);
                robots.Add(new(new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)), new(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))));
            }
            return robots;
        }

        [GeneratedRegex(@"p=(\d+),(\d+) v=(-?\d+),(-?\d+)")]
        private static partial Regex RobotRegex();

        private record class Robot(Coords Position, Coords Velocity)
        {
            public Coords Position { get; set; } = Position;
        }
    }
}
