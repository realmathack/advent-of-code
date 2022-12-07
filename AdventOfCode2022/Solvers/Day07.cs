using System.Reflection.Metadata.Ecma335;
using System.Security;

namespace AdventOfCode2022.Solvers
{
    public class Day07 : IBaseSolver
    {
        private int _totalSize = 0;
        private int _deleteSize = int.MaxValue;

        public string SolvePart1(string input)
        {
            var root = ParseInput(input);
            GetTotalSize(root);
            return _totalSize.ToString();
        }

        public string SolvePart2(string input)
        {
            var root = ParseInput(input);
            var total = GetTotalSize(root);
            var toDelete = 30000000 - (70000000 - total);
            FindDirToDelete(root, toDelete);
            return _deleteSize.ToString();
        }

        private int GetTotalSize(Dir current)
        {
            var currentSize = current.Files.Sum(file => file.Value);
            currentSize += current.Dirs.Sum(dir => GetTotalSize(dir.Value));
            if (currentSize <= 100000)
            {
                _totalSize += currentSize;
            }
            return currentSize;
        }
        
        private void FindDirToDelete(Dir current, int minRequiredSize)
        {
            var currentSize = current.Files.Sum(file => file.Value);
            currentSize += current.Dirs.Sum(dir => GetTotalSize(dir.Value));
            if (currentSize > minRequiredSize && currentSize <= _deleteSize)
            {
                _deleteSize = currentSize;
            }
            foreach (var dir in current.Dirs.Values)
            {
                FindDirToDelete(dir, minRequiredSize);
            }
        }

        private static Dir ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var root = new Dir("/");
            var current = new Stack<Dir>();
            foreach (var line in lines)
            {
                if (line == "$ ls")
                {
                    continue;
                }
                if (line == "$ cd /")
                {
                    current.Clear();
                    current.Push(root);
                    continue;
                }
                if (line == "$ cd ..")
                {
                    current.Pop();
                    continue;
                }
                if (line.StartsWith("$ cd"))
                {
                    var dirName = line.Substring(5);
                    var dir = current.Peek().Dirs[dirName];
                    current.Push(dir);
                }
                else
                {
                    var parts = line.Split(' ');
                    if (parts[0] == "dir")
                    {
                        var dir = new Dir(parts[1]);
                        current.Peek().Dirs.Add(dir.Name, dir);
                    }
                    else
                    {
                        var size = int.Parse(parts[0]);
                        current.Peek().Files.Add(parts[1], size);
                    }
                }
            }
            return root;
        }

        private class Dir
        {
            public string Name { get; set; }
            public Dictionary<string, Dir> Dirs { get; set; }
            public Dictionary<string, int> Files { get; set; }

            public Dir(string name)
            {
                Name = name;
                Dirs = new Dictionary<string, Dir>();
                Files = new Dictionary<string, int>();
            }
        }
    }
}
