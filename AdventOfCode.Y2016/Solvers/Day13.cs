namespace AdventOfCode.Y2016.Solvers
{
    public class Day13 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var designerFavNumber = int.Parse(input[0]);
            var parts = input[1].Split(',');
            var goal = new Coords(int.Parse(parts[0]), int.Parse(parts[1]));
            var current = new Coords(1, 1);
            _ = IsWall(current, designerFavNumber) && current != goal; // HACK: Discard to remove "variables not used" warnings
            return null!;
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Implement
            return null!;
        }

        private static bool IsWall(Coords coords, int designerFavNumber)
        {
            var value = coords.X * coords.X + 3 * coords.X + 2 * coords.X * coords.Y + coords.Y + coords.Y * coords.Y;
            value += designerFavNumber;
            var binary = Convert.ToString(value, 2);
            return binary.Count(c => c == '1') % 2 == 1;
        }
    }
}
