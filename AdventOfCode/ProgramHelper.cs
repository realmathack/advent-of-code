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

        public static ISolver CreateSolver(Type? type)
        {
            if (type is null)
            {
                throw new InvalidOperationException($"Type not found for day!");
            }
            if (Activator.CreateInstance(type) is ISolver solver)
            {
                return solver;
            }
            throw new InvalidOperationException($"Type {type.FullName} is not a {nameof(ISolver)}!");
        }

        public static void Run(this ISolver solver, string day)
        {
            solver.SetInput(File.ReadAllText(@$"inputs\{day}.txt"));
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var part1Answer = solver.SolvePart1();
            var part1Time = sw.Elapsed;
            sw.Restart();
            var part2Answer = solver.SolvePart2();
            var part2Time = sw.Elapsed;
            sw.Stop();
            Console.WriteLine($"Part1:\t\t{part1Answer}");
            Console.WriteLine($"Part2:\t\t{part2Answer}");
            Console.WriteLine();
            Console.WriteLine($"Part1 in {part1Time.TotalMilliseconds}ms");
            Console.WriteLine($"Part2 in {part2Time.TotalMilliseconds}ms");
            Console.WriteLine();
        }
    }
}
