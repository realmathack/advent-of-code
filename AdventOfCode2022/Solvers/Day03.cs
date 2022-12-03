namespace AdventOfCode2022.Solvers
{
    public class Day03 : IBaseSolver
    {
        public string SolvePart1(string input)
        {
            var rucksacks = ParseIntoCompartments(input);
            var total = 0;
            foreach (var rucksack in rucksacks)
            {
                foreach (var item in rucksack.Item1)
                {
                    if (rucksack.Item2.Contains(item))
                    {
                        total += GetPriority(item);
                        break;
                    }
                }
            }
            return total.ToString();
        }

        public string SolvePart2(string input)
        {
            var groups = ParseIntoGroups(input);
            var total = 0;
            foreach (var group in groups)
            {
                foreach (var item in group[0])
                {
                    if (group[1].Contains(item) && group[2].Contains(item))
                    {
                        total += GetPriority(item);
                        break;
                    }
                }
            }
            return total.ToString();
        }

        private static List<(string, string)> ParseIntoCompartments(string input)
        {
            var result = new List<(string, string)>();
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var compartmentLength = line.Length / 2;
                var compartment1 = line.Substring(0, compartmentLength);
                var compartment2 = line.Substring(compartmentLength, compartmentLength);
                result.Add((compartment1, compartment2));
            }
            return result;
        }

        private static List<List<string>> ParseIntoGroups(string input)
        {
            var result = new List<List<string>>();
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var groupCount = lines.Length / 3;
            for (int i = 0; i < groupCount; i++)
            {
                result.Add(new List<string>(lines.Skip(i * 3).Take(3)));
            }
            return result;
        }

        private static int GetPriority(char item)
        {
            return (item > 'Z') ? item - 96 : item - 38;
        }
    }
}
