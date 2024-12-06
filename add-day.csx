static string Getday()
{
    string? numberInput;
    int day;
    do
    {
        Console.Write("Type day number: ");
        numberInput = Console.ReadLine();
    } while (!int.TryParse(numberInput, out day) || day < 1 || day > 25);
    return day.ToString("00");
}

var year = Args[0];
var day = Getday();
File.Copy(@"templates\00.txt", @$"AdventOfCode.Y{year}\inputs\{day}.txt");

var testClass = File.ReadAllText(@"templates\Test00.cs");
testClass = testClass.Replace("2000", year);
testClass = testClass.Replace("00", day);
File.WriteAllText(@$"AdventOfCode.Y{year}.Tests\Test{day}.cs", testClass);

var dayClass = File.ReadAllText(@"templates\Day00.cs");
dayClass = dayClass.Replace("2000", year);
dayClass = dayClass.Replace("00", day);
File.WriteAllText(@$"AdventOfCode.Y{year}\Solvers\Day{day}.cs", dayClass);