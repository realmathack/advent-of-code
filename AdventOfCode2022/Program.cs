using AdventOfCode2022;
using System.Diagnostics;

//var dayNumber = "01";
var dayNumber = GetDayNumber();
var solver = GetSolver(dayNumber);
var input = GetInput(dayNumber);

var stopwatch = new Stopwatch();
stopwatch.Start();
Console.WriteLine($"Part1:\t{solver.SolvePart1(input)}");
Console.WriteLine($"Part2:\t{solver.SolvePart2(input)}");
stopwatch.Stop();
Console.WriteLine();
Console.WriteLine($"Time:\t{stopwatch.ElapsedMilliseconds}ms");

static string GetDayNumber()
{
    string? numberInput;
    int dayNumber;
    do
    {
        Console.Write("Type day number: ");
        numberInput = Console.ReadLine();
    } while (!int.TryParse(numberInput, out dayNumber) || dayNumber < 1 || dayNumber > 25);
    return dayNumber.ToString("00");
}

static IBaseSolver GetSolver(string dayNumber)
{
    var typeName = $"AdventOfCode2022.Solvers.Day{dayNumber}";
    var type = Type.GetType(typeName);
    if (type is null)
    {
        throw new InvalidOperationException($"Type not found: {typeName}");
    }
    if (Activator.CreateInstance(type) is IBaseSolver solver)
    {
        return solver;
    }
    throw new InvalidOperationException($"Type {typeName} is not a IBaseSolver");
}

static string GetInput(string dayNumber)
{
    return File.ReadAllText(@$"inputs\{dayNumber}.txt");
}