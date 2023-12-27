namespace AdventOfCode.Y2023.Solvers
{
    public class Day19 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var (workflows, parts) = ToWorkflowsAndParts(input);
            var sum = 0;
            foreach (var part in parts)
            {
                var workflow = workflows["in"];
                while (ExecuteWorkflow(workflow, part, out var action))
                {
                    if (action == "A")
                    {
                        sum += part.Sum(rating => rating.Value);
                        break;
                    }
                    workflow = workflows[action];
                }
            }
            return sum;
        }

        public override object SolvePart2(string[] input)
        {
            var (workflows, _) = ToWorkflowsAndParts(input);
            var queue = new Queue<WorkflowInput>();
            queue.Enqueue(new("in", new() { { 'x', new(1, 4000) }, { 'm', new(1, 4000) }, { 'a', new(1, 4000) }, { 's', new(1, 4000) } }));
            var accepted = new List<WorkflowInput>();
            while (queue.TryDequeue(out var current))
            {
                foreach (var condition in workflows[current.Name].Conditions)
                {
                    if (condition.Category is null)
                    {
                        if (condition.Action == "A")
                        {
                            accepted.Add(current);
                        }
                        else if (condition.Action != "R")
                        {
                            queue.Enqueue(new(condition.Action, current.Ranges));
                        }
                    }
                    else
                    {
                        if (condition.Comparison == '<')
                        {
                            if (current.Ranges[condition.Category.Value].End < condition.Value)
                            {
                                if (condition.Action == "A")
                                {
                                    accepted.Add(current);
                                }
                                else if (condition.Action != "R")
                                {
                                    queue.Enqueue(new(condition.Action, current.Ranges));
                                }
                            }
                            else if (current.Ranges[condition.Category.Value].Start < condition.Value)
                            {
                                var tmp = current.Duplicate();
                                tmp.Ranges[condition.Category.Value] = new(tmp.Ranges[condition.Category.Value].Start, condition.Value.Value - 1);
                                current.Ranges[condition.Category.Value] = new(condition.Value.Value, current.Ranges[condition.Category.Value].End);
                                if (condition.Action == "A")
                                {
                                    accepted.Add(tmp);
                                }
                                else if (condition.Action != "R")
                                {
                                    queue.Enqueue(new(condition.Action, tmp.Ranges));
                                }
                            }
                        }
                        else
                        {
                            if (current.Ranges[condition.Category.Value].Start > condition.Value)
                            {
                                if (condition.Action == "A")
                                {
                                    accepted.Add(current);
                                }
                                else if (condition.Action != "R")
                                {
                                    queue.Enqueue(new(condition.Action, current.Ranges));
                                }
                            }
                            else if (current.Ranges[condition.Category.Value].End > condition.Value)
                            {
                                var tmp = current.Duplicate();
                                tmp.Ranges[condition.Category.Value] = new(condition.Value.Value + 1, tmp.Ranges[condition.Category.Value].End);
                                current.Ranges[condition.Category.Value] = new(current.Ranges[condition.Category.Value].Start, condition.Value.Value);
                                if (condition.Action == "A")
                                {
                                    accepted.Add(tmp);
                                }
                                else if (condition.Action != "R")
                                {
                                    queue.Enqueue(new(condition.Action, tmp.Ranges));
                                }
                            }
                        }
                    }
                }
            }
            var sum = 0L;
            foreach (var item in accepted)
            {
                sum += (long)item.Ranges['x'].Length * item.Ranges['m'].Length * item.Ranges['a'].Length * item.Ranges['s'].Length;
            }
            return sum;
        }

        private static bool ExecuteWorkflow(Workflow workflow, Dictionary<char, int> part, out string action)
        {
            action = "R";
            foreach (var condition in workflow.Conditions)
            {
                if (condition.Category is null)
                {
                    action = condition.Action;
                    break;
                }
                if (condition.Comparison == '<')
                {
                    if (part[condition.Category.Value] < condition.Value)
                    {
                        action = condition.Action;
                        break;
                    }
                }
                else
                {
                    if (part[condition.Category.Value] > condition.Value)
                    {
                        action = condition.Action;
                        break;
                    }
                }
            }
            return action != "R";
        }

        private static (Dictionary<string, Workflow> Workflows, List<Dictionary<char, int>> Parts) ToWorkflowsAndParts(string[] sections)
        {
            var workflows = new Dictionary<string, Workflow>();
            foreach (var workflow in sections[0].SplitIntoLines())
            {
                var conditions = new List<Condition>();
                var (name, rest) = workflow.SplitInTwo('{');
                foreach (var condition in rest[..^1].Split(','))
                {
                    int pos;
                    if ((pos = condition.IndexOf(':')) == -1)
                    {
                        conditions.Add(new(condition));
                    }
                    else
                    {
                        conditions.Add(new(condition[(pos + 1)..], condition[0], condition[1], int.Parse(condition[2..pos])));
                    }
                }
                workflows.Add(name, new(name, conditions));
            }
            var parts = sections[1].SplitIntoLines()
                .Select(part => part[1..^1].Split(',').Select(rating => (Category: rating[0], Value: int.Parse(rating[2..]))).ToDictionary())
                .ToList();
            return (workflows, parts);
        }

        private record class Workflow(string Name, List<Condition> Conditions);
        private record class Condition(string Action, char? Category = null, char? Comparison = null, int? Value = null);
        private record class WorkflowInput(string Name, Dictionary<char, Range<int>> Ranges)
        {
            public WorkflowInput Duplicate() => new(Name, Ranges.Select(kv => (kv.Key, kv.Value)).ToDictionary());
        }
    }
}
