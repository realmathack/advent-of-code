var day = AdventOfCode.ProgramHelper.GetDayFromInput("2023");
var type = Type.GetType($"AdventOfCode.Y2023.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);