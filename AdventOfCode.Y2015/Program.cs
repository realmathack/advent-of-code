var day = AdventOfCode.ProgramHelper.GetDayFromInput("2015");
var type = Type.GetType($"AdventOfCode.Y2015.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);