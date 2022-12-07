static string GetDayNumber()
{
    string? numberInput;
    int dayNumber;
    do
    {
        Console.Write("Type day number: ");
        numberInput = Console.ReadLine();
    } while (!int.TryParse(numberInput, out dayNumber) || dayNumber < 1 || dayNumber > 25);
    return dayNumber.ToString("00");
}

var dayNumber = GetDayNumber();
File.Copy(@"templates\00.txt", @$"AdventOfCode.Y2022\inputs\{dayNumber}.txt");

var testClass = File.ReadAllText(@"templates\Test00.cs");
testClass = testClass.Replace("00", dayNumber);
File.WriteAllText(@$"AdventOfCode.Y2022.Tests\Test{dayNumber}.cs", testClass);

var dayClass = File.ReadAllText(@"templates\Day00.cs");
dayClass = dayClass.Replace("00", dayNumber);
File.WriteAllText(@$"AdventOfCode.Y2022\Solvers\Day{dayNumber}.cs", dayClass);

var projectFile = File.ReadAllText(@"AdventOfCode.Y2022\AdventOfCode.Y2022.csproj");
projectFile = projectFile.Insert(projectFile.IndexOf("</ItemGroup>"), $"  <None Update=\"inputs\\{dayNumber}.txt\">\r\n      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\r\n    </None>\r\n  ");
File.WriteAllText(@"AdventOfCode.Y2022\AdventOfCode.Y2022.csproj", projectFile);