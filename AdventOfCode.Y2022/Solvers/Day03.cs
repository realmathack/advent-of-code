namespace AdventOfCode.Y2022.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var rucksacks = GetRucksacks(input);
            var total = 0;
            foreach (var rucksack in rucksacks)
            {
                foreach (var item in rucksack.first)
                {
                    if (rucksack.second.Contains(item))
                    {
                        total += GetPriority(item);
                        break;
                    }
                }
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var groups = GetGroups(input);
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
            return total;
        }

        private static List<(string first, string second)> GetRucksacks(string[] lines)
        {
            var result = new List<(string, string)>(lines.Length);
            foreach (var line in lines)
            {
                var length = line.Length / 2;
                result.Add((line[..length], line[length..]));
            }
            return result;
        }

        private static List<string[]> GetGroups(string[] lines)
        {
            var groupCount = lines.Length / 3;
            var result = new List<string[]>(groupCount);
            for (int i = 0; i < groupCount; i++)
            {
                result.Add(lines.Skip(i * 3).Take(3).ToArray());
            }
            return result;
        }

        private static int GetPriority(char item)
        {
            return (item > 'Z') ? item - 'a' + 1 : item - 'A' + 27;
        }
    }
}
