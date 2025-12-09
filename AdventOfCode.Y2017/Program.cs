var day = AdventOfCode.ProgramHelper.GetDayFromInput("2017");
var type = Type.GetType($"AdventOfCode.Y2017.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);