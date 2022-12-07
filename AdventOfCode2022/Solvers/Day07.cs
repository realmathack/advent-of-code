namespace AdventOfCode2022.Solvers
{
    public class Day07 : IBaseSolver
    {
        public string SolvePart1(string input)
        {
            var data = ParseInput(input);
            var total = data.Where(x => x <= 100000).Sum();
            return total.ToString();
        }

        public string SolvePart2(string input)
        {
            var data = ParseInput(input);
            var toDelete = 30000000 - (70000000 - data.Max());
            var total = data.Where(x => x > toDelete).Min();
            return total.ToString();
        }

        private static List<int> ParseInput(string input)
        {
            var result = new Dictionary<string, int>();
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var current = string.Empty;
            foreach (var line in lines)
            {
                if (line.StartsWith("$ ls") || line.StartsWith("dir"))
                {
                    continue;
                }
                if (line.StartsWith("$ cd"))
                {
                    var dirChange = line[5..];
                    current = ExecuteDirChange(current, dirChange);
                    if (!result.ContainsKey(current))
                    {
                        result.Add(current, 0);
                    }
                }
                else
                {
                    var parts = line.Split(' ');
                    var size = int.Parse(parts[0]);
                    foreach (var folder in GetCurrentAndParentFolders(current))
                    {
                        result[folder] += size;
                    }
                }
            }
            return result.Values.ToList();
        }

        private static string ExecuteDirChange(string current, string dirChange)
        {
            if (dirChange == "/")
            {
                return dirChange;
            }
            if (dirChange == "..")
            {
                return RemoveLastFolder(current);
            }
            return string.Concat(current, dirChange, "/");
        }

        private static string RemoveLastFolder(string current)
        {
            if (current == "/")
            {
                return current;
            }
            var pos = current.LastIndexOf('/', current.Length - 2);
            return current[..(pos + 1)];
        }

        private static IEnumerable<string> GetCurrentAndParentFolders(string current)
        {
            yield return current;
            while (current != "/")
            {
                current = RemoveLastFolder(current);
                yield return current;
            }
        }
    }
}
