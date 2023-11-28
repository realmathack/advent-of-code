global using AdventOfCode;
global using System.Text;

var year = "2000";
var day = ProgramHelper.GetDayFromInput(year);
var type = Type.GetType($"AdventOfCode.Y{year}.Solvers.Day{day}");
ProgramHelper.CreateSolver(type).Run(day);