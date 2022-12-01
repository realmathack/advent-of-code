var input = File.ReadAllLines("input.txt");
var results = new List<int>();
var total = 0;

foreach (var line in input)
{
	if (string.IsNullOrWhiteSpace(line))
	{
		results.Add(total);
		total = 0;
		continue;
	}
	if (int.TryParse(line, out int current))
	{
		total += current;
	}
	else
	{
		Console.WriteLine($"Not an integer value: {line}");
	}
}

results = results.OrderByDescending(x => x).ToList();

var highest = results[0];
var top3 = results[0] + results[1] + results[2];

Console.WriteLine(highest);
Console.WriteLine(top3);
