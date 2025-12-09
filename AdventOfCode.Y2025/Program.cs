var day = AdventOfCode.ProgramHelper.GetDayFromInput("2025");
var type = Type.GetType($"AdventOfCode.Y2025.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);