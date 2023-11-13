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
            do
            {
                var program = toProcess.Dequeue();
                if (!connected.Contains(program))
                {
                    connected.Add(program);
                    programs[program].ForEach(toProcess.Enqueue);
                }
            } while (toProcess.Count > 0);
            return connected.Count;
        }

        public override object SolvePart2(string[] input)
        {
            var programs = ToPrograms(input);
            var connected = new Dictionary<int, HashSet<int>>();
            do
            {
                var root = programs.First().Key;
                connected.Add(root, new() { root });
                var toProcess = new Queue<int>();
                programs[root].ForEach(toProcess.Enqueue);
                programs.Remove(root);
                do
                {
                    var program = toProcess.Dequeue();
                    if (!connected[root].Contains(program))
                    {
                        connected[root].Add(program);
                        programs[program].ForEach(toProcess.Enqueue);
                        programs.Remove(program);
                    }
                } while (toProcess.Count > 0);
            } while (programs.Count > 0);
            return connected.Count;
        }

        private Dictionary<int, List<int>> ToPrograms(string[] input)
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
