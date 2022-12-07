using AdventOfCode2022.Abstractions;
using System.Diagnostics;

//var day = "01";
var day = GetDay();
var solver = GetSolver(day);
solver.SetInput(File.ReadAllText(@$"inputs\{day}.txt"));

var sw = new Stopwatch();
sw.Start();
var part1Answer = solver.SolvePart1();
var part1Time = sw.Elapsed;
sw.Restart();
var part2Answer = solver.SolvePart2();
var part2Time = sw.Elapsed;
sw.Stop();
Console.WriteLine();
Console.WriteLine($"Part1:\t{part1Answer}\t\t\tin {part1Time.TotalMilliseconds}ms");
Console.WriteLine($"Part2:\t{part2Answer}\t\t\tin {part2Time.TotalMilliseconds}ms");
Console.WriteLine();

static string GetDay()
{
    string? input;
    int day;
    do
    {
        Console.Write("Show solutions for day: ");
        input = Console.ReadLine();
    } while (!int.TryParse(input, out day) || day < 1 || day > 25);
    return day.ToString("D2");
}

static ISolver GetSolver(string day)
{
    var type = Type.GetType($"AdventOfCode2022.Solvers.Day{day}");
    if (type is null)
    {
        throw new InvalidOperationException($"Type not found for day {day}");
    }
    if (Activator.CreateInstance(type) is ISolver solver)
    {
        return solver;
    }
    throw new InvalidOperationException($"Type {type.FullName} is not a IBaseSolver");
}