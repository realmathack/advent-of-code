var day = AdventOfCode.ProgramHelper.GetDayFromInput("2018");
var type = Type.GetType($"AdventOfCode.Y2018.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);