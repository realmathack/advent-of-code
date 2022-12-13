namespace AdventOfCode.Y2016.Solvers
{
    public class Day07 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            return input.Count(SupportsTls);
        }

        public override object SolvePart2(string[] input)
        {
            return input.Count(SupportsSsl);
        }

        private static bool SupportsTls(string line)
        {
            var (inside, outside) = GetBracketParts(line);
            if (inside.Any(ContainsAbba))
            {
                return false;
            }
            return outside.Any(ContainsAbba);
        }

        private static bool ContainsAbba(string part)
        {
            for (int i = 0; i < part.Length - 3; i++)
            {
                if (part[i] == part[i + 3] && part[i] != part[i + 1] && part[i + 1] == part[i + 2])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool SupportsSsl(string line)
        {
            var (inside, outside) = GetBracketParts(line);
            var possibleBabs = GetPossibleBabs(outside);
            foreach (var bab in possibleBabs)
            {
                if (inside.Any(x => x.Contains(bab)))
                {
                    return true;
                }
            }
            return false;
        }

        private static List<string> GetPossibleBabs(List<string> outside)
        {
            var babs = new List<string>();
            foreach (var part in outside)
            {
                for (int i = 0; i < part.Length - 2; i++)
                {
                    if (part[i] == part[i + 2] && part[i] != part[i + 1])
                    {
                        babs.Add(string.Join("", part[i + 1], part[i], part[i + 1]));
                    }
                }
            }
            return babs;
        }

        private static (List<string> inside, List<string> outside) GetBracketParts(string line)
        {
            var inside = new List<string>();
            var outside = new List<string>();
            var current = 0;
            var pos = 0;
            while ((pos = line.IndexOf('[', current)) != -1)
            {
                outside.Add(line[current..pos]);
                var end = line.IndexOf(']', pos);
                inside.Add(line[(pos + 1)..end]);
                current = end + 1;
            }
            outside.Add(line[current..]);
            return (inside, outside);
        }
    }
}
