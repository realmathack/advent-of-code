var day = AdventOfCode.ProgramHelper.GetDayFromInput("2021");
var type = Type.GetType($"AdventOfCode.Y2021.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);