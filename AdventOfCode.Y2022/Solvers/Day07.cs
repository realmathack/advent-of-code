namespace AdventOfCode.Y2022.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => CalculateFolderSizes(input).Where(size => size <= 100000).Sum();

        public override object SolvePart2(string[] input)
        {
            var sizes = CalculateFolderSizes(input);
            var toDelete = 30000000 - (70000000 - sizes.Max());
            return sizes.Where(size => size > toDelete).Min();
        }

        private static List<int> CalculateFolderSizes(string[] lines)
        {
            var result = new Dictionary<string, int>();
            var current = string.Empty;
            foreach (var line in lines)
            {
                if (line.StartsWith("$ ls") || line.StartsWith("dir"))
                {
                    continue;
                }
                if (line.StartsWith("$ cd"))
                {
                    current = ExecuteDirChange(current, line[5..]);
                    result.TryAdd(current, 0);
                }
                else
                {
                    var size = int.Parse(line.Split(' ')[0]);
                    foreach (var folder in GetCurrentAndParentFolders(current))
                    {
                        result[folder] += size;
                    }
                }
            }
            return [.. result.Values];
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
            var end = current.LastIndexOf('/', current.Length - 2) + 1;
            return current[..end];
        }

        private static IEnumerable<string> GetCurrentAndParentFolders(string current)
        {
            yield return current;
            while (current != "/")
            {
                yield return current = RemoveLastFolder(current);
            }
        }
    }
}
