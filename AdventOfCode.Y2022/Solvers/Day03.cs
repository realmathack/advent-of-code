namespace AdventOfCode.Y2022.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var rucksacks = ToRucksacks(input);
            var total = 0;
            foreach (var (first, second) in rucksacks)
            {
                foreach (var item in first)
                {
                    if (second.Contains(item))
                    {
                        total += CalculatePriority(item);
                        break;
                    }
                }
            }
            return total;
        }

        public override object SolvePart2(string[] input)
        {
            var groups = ToGroups(input);
            var total = 0;
            foreach (var group in groups)
            {
                foreach (var item in group[0])
                {
                    if (group[1].Contains(item) && group[2].Contains(item))
                    {
                        total += CalculatePriority(item);
                        break;
                    }
                }
            }
            return total;
        }

        private static List<(string First, string Second)> ToRucksacks(string[] lines)
        {
            var rucksacks = new List<(string First, string Second)>(lines.Length);
            foreach (var line in lines)
            {
                var length = line.Length / 2;
                rucksacks.Add((line[..length], line[length..]));
            }
            return rucksacks;
        }

        private static List<string[]> ToGroups(string[] lines)
        {
            var groupCount = lines.Length / 3;
            var groups = new List<string[]>(groupCount);
            for (int i = 0; i < groupCount; i++)
            {
                groups.Add(lines.Skip(i * 3).Take(3).ToArray());
            }
            return groups;
        }

        private static int CalculatePriority(char item)
        {
            return (item > 'Z') ? item - 'a' + 1 : item - 'A' + 27;
        }
    }
}
