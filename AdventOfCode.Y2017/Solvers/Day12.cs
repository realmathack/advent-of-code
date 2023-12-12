namespace AdventOfCode.Y2017.Solvers
{
    public class Day12 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var programs = ToPrograms(input);
            var connected = new HashSet<int>() { 0 };
            var toProcess = new Queue<int>();
            programs[0].ForEach(toProcess.Enqueue);
            while (toProcess.TryDequeue(out var program))
            {
                if (connected.Add(program))
                {
                    programs[program].ForEach(toProcess.Enqueue);
                }
            }
            return connected.Count;
        }

        public override object SolvePart2(string[] input)
        {
            var programs = ToPrograms(input);
            var connected = new Dictionary<int, HashSet<int>>();
            while (programs.Count > 0)
            {
                var root = programs.First().Key;
                connected.Add(root, [root]);
                var toProcess = new Queue<int>();
                programs[root].ForEach(toProcess.Enqueue);
                programs.Remove(root);
                while (toProcess.TryDequeue(out var program))
                {
                    if (connected[root].Add(program))
                    {
                        programs[program].ForEach(toProcess.Enqueue);
                        programs.Remove(program);
                    }
                }
            }
            return connected.Count;
        }

        private static Dictionary<int, List<int>> ToPrograms(string[] input)
        {
            var programs = new Dictionary<int, List<int>>();
            foreach (var line in input)
            {
                var parts = line.Split(" <-> ");
                var neighbors = parts[1].Split(", ").Select(int.Parse).ToList();
                programs.Add(int.Parse(parts[0]), neighbors);
            }
            return programs;
        }
    }
}
