var day = AdventOfCode.ProgramHelper.GetDayFromInput("2020");
var type = Type.GetType($"AdventOfCode.Y2020.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);