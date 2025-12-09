var day = AdventOfCode.ProgramHelper.GetDayFromInput("2000");
var type = Type.GetType($"AdventOfCode.Y2000.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);