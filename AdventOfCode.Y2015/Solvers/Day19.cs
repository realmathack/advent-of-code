namespace AdventOfCode.Y2015.Solvers
{
    public class Day19 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var molecule = input[1].Trim();
            var replacements = ToReplacements(input[0]);
            return GetAllPossibleVariants(replacements, molecule).Distinct().Count();
        }

        public override object SolvePart2(string[] input)
        {
            var molecule = input[1].Trim();
            var replacements = ToReplacements(input[0], true);
            var start = molecule;
            var steps = 0;
            while (molecule != "e")
            {
                var variants = GetAllPossibleVariants(replacements, molecule).ToList();
                if (variants.Count == 0)
                {
                    molecule = start;
                    steps = 0;
                    continue;
                }
                molecule = variants[Random.Shared.Next(variants.Count)];
                steps++;
            }
            return steps;
        }

        private static IEnumerable<string> GetAllPossibleVariants(List<Replacement> replacements, string input)
        {
            foreach (var replacement in replacements)
            {
                int pos = 0;
                while ((pos = input.IndexOf(replacement.Find, pos)) != -1)
                {
                    yield return input[..pos] + replacement.Replace + input[(pos++ + replacement.Find.Length)..];
                }
            }
        }

        private static List<Replacement> ToReplacements(string input, bool reduce = false)
        {
            var replacements = new List<Replacement>();
            foreach (var line in input.SplitIntoLines())
            {
                var parts = line.Split(" => ");
                if (reduce)
                {
                    Array.Reverse(parts);
                }
                replacements.Add(new(parts[0], parts[1]));
            }
            return replacements;
        }

        private record struct Replacement(string Find, string Replace);
    }
}
