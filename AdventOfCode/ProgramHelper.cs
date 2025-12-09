using Stopwatch = System.Diagnostics.Stopwatch;

namespace AdventOfCode
{
    public static class ProgramHelper
    {
        public static string GetDayFromInput(string year)
        {
            string? input;
            int day;
            do
            {
                Console.Write($"Show solutions for year {year}, day: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out day) || day < 1 || day > 25);
            Console.WriteLine();
            return day.ToString("D2");
        }

        public static void Run(Type? type, string day)
        {
            ArgumentNullException.ThrowIfNull(type);
            if (Activator.CreateInstance(type) is not ISolver solver)
            {
                throw new InvalidOperationException($"Type {type.FullName} is not an {nameof(ISolver)}!");
            }
            var startTime = Stopwatch.GetTimestamp();
            solver.SetInput(File.ReadAllText(@$"inputs\{day}.txt"));
            var inputTime = Stopwatch.GetElapsedTime(startTime);
            startTime = Stopwatch.GetTimestamp();
            var part1Answer = solver.SolvePart1();
            var part1Time = Stopwatch.GetElapsedTime(startTime);
            startTime = Stopwatch.GetTimestamp();
            var part2Answer = solver.SolvePart2();
            var part2Time = Stopwatch.GetElapsedTime(startTime);
            Console.WriteLine($"Part1:\t\t{part1Answer}");
            Console.WriteLine($"Part2:\t\t{part2Answer}");
            Console.WriteLine();
            Console.WriteLine($"Part1 in {part1Time.TotalMilliseconds}ms");
            Console.WriteLine($"Part2 in {part2Time.TotalMilliseconds}ms");
            Console.WriteLine($"Input in {inputTime.TotalMilliseconds}ms (read + basic parse)");
            Console.WriteLine();
        }
    }
}
