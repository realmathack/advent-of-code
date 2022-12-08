global using AdventOfCode;

var year = "2016";
//var day = "01";
var day = MainHelper.GetDayFromInput(year);
var type = Type.GetType($"AdventOfCode.Y{year}.Solvers.Day{day}");
MainHelper.CreateSolver(type).Run(day);