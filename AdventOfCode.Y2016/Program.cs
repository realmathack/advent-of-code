var day = AdventOfCode.ProgramHelper.GetDayFromInput("2016");
var type = Type.GetType($"AdventOfCode.Y2016.Solvers.Day{day}");
AdventOfCode.ProgramHelper.Run(type, day);