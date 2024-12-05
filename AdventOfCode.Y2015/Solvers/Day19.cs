namespace AdventOfCode.Y2015.Solvers
{
    public class Day19 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input) => FindAllPossibleVariants(ToReplacements(input[0]), input[1].Trim()).Distinct().Count();

        public override object SolvePart2(string[] input)
        {
            var molecule = input[1].Trim();
            var replacements = ToReplacements(input[0], true);
            var start = molecule;
            var steps = 0;
            while (molecule != "e")
            {
                var variants = FindAllPossibleVariants(replacements, molecule).ToList();
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

        private static List<Replacement> ToReplacements(string lineGroup, bool reduce = false)
        {
            var replacements = new List<Replacement>();
            foreach (var line in lineGroup.SplitIntoLines())
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

        private static IEnumerable<string> FindAllPossibleVariants(List<Replacement> replacements, string molecule)
        {
            foreach (var replacement in replacements)
            {
                int pos = 0;
                while ((pos = molecule.IndexOf(replacement.Find, pos)) != -1)
                {
                    yield return molecule[..pos] + replacement.Replace + molecule[(pos++ + replacement.Find.Length)..];
                }
            }
        }

        private record class Replacement(string Find, string Replace);
    }
}
