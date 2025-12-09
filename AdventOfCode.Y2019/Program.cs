var day = AdventOfCode.ProgramHelper.GetDayFromInput("2019");
var type = Type.GetType($"AdventOfCode.Y2019.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);