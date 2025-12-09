var day = AdventOfCode.ProgramHelper.GetDayFromInput("2024");
var type = Type.GetType($"AdventOfCode.Y2024.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);