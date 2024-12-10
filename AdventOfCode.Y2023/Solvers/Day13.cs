namespace AdventOfCode.Y2023.Solvers
{
    public class Day13 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input) => input.Sum(lineGroup => FindReflection(lineGroup));
        public override object SolvePart2(string[] input) => input.Sum(FindSmudge);

        private static int FindSmudge(string lineGroup)
        {
            var oldReflection = FindReflection(lineGroup);
            for (int i = 0; i < lineGroup.Length; i++)
            {
                if (lineGroup[i] != '.' && lineGroup[i] != '#')
                {
                    continue;
                }
                var tmp = lineGroup.ToCharArray();
                tmp[i] = (tmp[i] == '#') ? '.' : '#';
                var reflection = FindReflection(new string(tmp), oldReflection);
                if (reflection > 0)
                {
                    return reflection;
                }
            }
            throw new InvalidOperationException("Smudge not found");
        }

        private static int FindReflection(string lineGroup, int oldReflection = -1)
        {
            var rows = lineGroup.SplitIntoLines();
            for (int row = 1; row < rows.Length; row++)
            {
                if (IsMirror(rows, row - 1, row) && oldReflection != 100 * row)
                {
                    return 100 * row;
                }
            }
            var columns = new string[rows[0].Length];
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = string.Concat(rows.Select(row => row[i]));
            }
            for (int col = 1; col < columns.Length; col++)
            {
                if (IsMirror(columns, col - 1, col) && oldReflection != col)
                {
                    return col;
                }
            }
            return 0;
        }

        private static bool IsMirror(string[] list, int first, int second)
        {
            var toCheck = Math.Min(first + 1, list.Length - second);
            for (int i = 0; i < toCheck; i++)
            {
                if (list[first - i] != list[second + i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
