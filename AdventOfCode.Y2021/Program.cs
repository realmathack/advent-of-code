﻿global using AdventOfCode;

var year = "2021";
var day = ProgramHelper.GetDayFromInput(year);
var type = Type.GetType($"AdventOfCode.Y{year}.Solvers.Day{day}");
ProgramHelper.CreateSolver(type).Run(day);