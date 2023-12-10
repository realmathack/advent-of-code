namespace AdventOfCode.Y2018.Solvers
{
    public class Day07(int _workers, int _seconds) : SolverWithLines
    {
        public Day07() : this(5, 60) { }

        public override object SolvePart1(string[] input)
        {
            var requirements = ToRequirements(ToInstructions(input));
            var order = string.Empty;
            var done = new HashSet<char>();
            var available = requirements.Values.SelectMany(steps => steps).Except(requirements.Keys).ToHashSet();
            while (available.Count > 0)
            {
                var current = available.OrderBy(step => step).First();
                order += current;
                done.Add(current);
                available.Remove(current);
                var steps = requirements.Where(req => req.Value.All(step => done.Contains(step))).Select(req => req.Key).ToList();
                foreach (var step in steps.Where(step => !done.Contains(step)))
                {
                    available.Add(step);
                }
            }
            return order;
        }

        public override object SolvePart2(string[] input)
        {
            var requirements = ToRequirements(ToInstructions(input));
            var seconds = -1;
            var workers = new Worker[_workers];
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new();
            }
            var done = new HashSet<char>();
            var available = requirements.Values.SelectMany(steps => steps).Except(requirements.Keys).ToHashSet();
            while (available.Count > 0 || workers.Any(worker => worker.Remaining > 0))
            {
                foreach (var worker in workers)
                {
                    if (--worker.Remaining > 0)
                    {
                        continue;
                    }
                    if (worker.Step != default)
                    {
                        done.Add(worker.Step);
                        worker.Step = default;
                        var steps = requirements.Where(req => req.Value.All(step => done.Contains(step))).Select(req => req.Key).ToList();
                        foreach (var step in steps.Where(step => !done.Contains(step) && !workers.Any(worker => worker.Step == step)))
                        {
                            available.Add(step);
                        }
                    }
                    var next = available.OrderBy(step => step).FirstOrDefault();
                    if (next == default)
                    {
                        continue;
                    }
                    available.Remove(next);
                    worker.Step = next;
                    worker.Remaining = next - 'A' + _seconds + 1;
                }
                seconds++;
            }
            return seconds;
        }

        private static Dictionary<char, HashSet<char>> ToRequirements(List<Instruction> instructions)
        {
            var requirements = new Dictionary<char, HashSet<char>>();
            foreach (var instruction in instructions)
            {
                if (!requirements.TryGetValue(instruction.Step, out var to))
                {
                    to = [];
                    requirements[instruction.Step] = to;
                }
                to.Add(instruction.Requires);
            }
            return requirements;
        }

        private static List<Instruction> ToInstructions(string[] input)
        {
            var instructions = new List<Instruction>();
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                instructions.Add(new(parts[7][0], parts[1][0]));
            }
            return instructions;
        }

        private record class Instruction(char Step, char Requires);
        private class Worker
        {
            public char Step { get; set; }
            public int Remaining { get; set; }
        }
    }
}
