static string GetYear()
{
    string? numberInput;
    int year;
    do
    {
        Console.Write("Type year: ");
        numberInput = Console.ReadLine();
    } while (!int.TryParse(numberInput, out year) || year < 2000 || year > 2099);
    return year.ToString();
}

var day = "01";
var year = GetYear();
var utf8Encoding = new UTF8Encoding(false);
var utf8BomEncoding = new UTF8Encoding(true);

if (Directory.Exists($"AdventOfCode.Y{year}") || Directory.Exists($"AdventOfCode.Y{year}.Tests"))
{
    Console.WriteLine($"Year {year} already exists");
    return;
}
Directory.CreateDirectory(@$"AdventOfCode.Y{year}\inputs");
Directory.CreateDirectory(@$"AdventOfCode.Y{year}\Solvers");
Directory.CreateDirectory($"AdventOfCode.Y{year}.Tests");

// day files
File.Copy(@"templates\00.txt", @$"AdventOfCode.Y{year}\inputs\{day}.txt");

var dayClass = File.ReadAllText(@"templates\Day00.cs");
dayClass = dayClass.Replace("2000", year);
dayClass = dayClass.Replace("00", day);
File.WriteAllText(@$"AdventOfCode.Y{year}\Solvers\Day{day}.cs", dayClass, utf8BomEncoding);

var testClass = File.ReadAllText(@"templates\Test00.cs");
testClass = testClass.Replace("2000", year);
testClass = testClass.Replace("00", day);
File.WriteAllText(@$"AdventOfCode.Y{year}.Tests\Test{day}.cs", testClass, utf8BomEncoding);

// year files
var addDayScript = File.ReadAllText(@"templates\year\add-day-2000.cmd");
addDayScript = addDayScript.Replace("2000", year);
File.WriteAllText(@$"add-day-{year}.cmd", addDayScript, utf8Encoding);

var answersFile = File.ReadAllText(@"templates\year\Answers-2000.md");
answersFile = answersFile.Replace("2000", year);
File.WriteAllText(@$"Answers-{year}.md", answersFile, utf8Encoding);

var programFile = File.ReadAllText(@"templates\year\AdventOfCode.Y2000\Program.cs");
programFile = programFile.Replace("2000", year);
File.WriteAllText(@$"AdventOfCode.Y{year}\Program.cs", programFile, utf8BomEncoding);

var usingsFile = File.ReadAllText(@"templates\year\AdventOfCode.Y2000.Tests\Usings.cs");
usingsFile = usingsFile.Replace("2000", year);
File.WriteAllText(@$"AdventOfCode.Y{year}.Tests\Usings.cs", usingsFile, utf8BomEncoding);

File.Copy(@"templates\year\AdventOfCode.Y2000\AdventOfCode.Y2000.csproj", @$"AdventOfCode.Y{year}\AdventOfCode.Y{year}.csproj");

var testProjectFile = File.ReadAllText(@"templates\year\AdventOfCode.Y2000.Tests\AdventOfCode.Y2000.Tests.csproj");
testProjectFile = testProjectFile.Replace("2000", year);
File.WriteAllText(@$"AdventOfCode.Y{year}.Tests\AdventOfCode.Y{year}.Tests.csproj", testProjectFile, utf8Encoding);

var solutionFile = File.ReadAllText("AdventOfCode.slnx");
solutionFile = solutionFile.Replace("    <File Path=\"README.md\" />", $"    <File Path=\"Answers-{year}.md\" />\r\n    <File Path=\"README.md\" />");
solutionFile = solutionFile.Replace("  <Project Path=\"AdventOfCode/AdventOfCode.csproj\" />", $"  <Project Path=\"AdventOfCode.Y{year}/AdventOfCode.Y{year}.csproj\" />\r\n  <Project Path=\"AdventOfCode.Y{year}.Tests/AdventOfCode.Y{year}.Tests.csproj\" />\r\n  <Project Path=\"AdventOfCode/AdventOfCode.csproj\" />");
File.WriteAllText("AdventOfCode.slnx", solutionFile, utf8Encoding);