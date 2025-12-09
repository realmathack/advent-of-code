var day = AdventOfCode.ProgramHelper.GetDayFromInput("2022");
var type = Type.GetType($"AdventOfCode.Y2022.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);