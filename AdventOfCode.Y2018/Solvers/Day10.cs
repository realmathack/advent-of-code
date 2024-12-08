using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Solvers
{
    public partial class Day10 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (lights, _) = FindSky(input);
            return ToMessage(lights);
        }

        public override object SolvePart2(string[] input)
        {
            var (_, seconds) = FindSky(input);
            return seconds;
        }

        private static (List<Light> Lights, long Seconds) FindSky(string[] lines)
        {
            var lights = ToLights(lines);
            var seconds = 0L;
            var last = long.MaxValue;
            var current = CalculateMessageArea(lights);
            while (last > current)
            {
                last = current;
                foreach (var light in lights)
                {
                    light.Position += light.Direction;
                }
                current = CalculateMessageArea(lights);
                seconds++;
            }
            foreach (var light in lights)
            {
                light.Position -= light.Direction;
            }
            seconds--;
            return (lights, seconds);
        }

        private static long CalculateMessageArea(List<Light> lights)
        {
            var minX = lights.Min(light => light.Position.X);
            var minY = lights.Min(light => light.Position.Y);
            var maxX = lights.Max(light => light.Position.X);
            var maxY = lights.Max(light => light.Position.Y);
            return (long)Math.Abs(maxX - minX) * Math.Abs(maxY - minY);
        }

        private static string ToMessage(List<Light> lights)
        {
            var minX = lights.Min(light => light.Position.X);
            var minY = lights.Min(light => light.Position.Y);
            var offset = new Coords(0 - minX, 0 - minY);
            foreach (var light in lights)
            {
                light.Position += offset;
            }
            var maxX = lights.Max(light => light.Position.X);
            var maxY = lights.Max(light => light.Position.Y);
            var screen = new Screen(maxX + 1, maxY + 1);
            foreach (var light in lights)
            {
                screen.SetPixel(light.Position.X, light.Position.Y);
            }
            return screen.PrintScreen();
        }

        private static List<Light> ToLights(string[] lines)
        {
            var lights = new List<Light>();
            foreach (var line in lines)
            {
                var match = LightRegex().Match(line);
                lights.Add(new(new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)), new(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))));
            }
            return lights;
        }

        [GeneratedRegex(@".+< *(-?\d+), +(-?\d+)>.+< *(-?\d+), +(-?\d+)>")]
        private static partial Regex LightRegex();

        private class Light(Coords position, Coords direction)
        {
            public Coords Position { get; set; } = position;
            public Coords Direction { get; } = direction;
        }
    }
}
