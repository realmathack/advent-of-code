namespace AdventOfCode.Y2020.Solvers
{
    public class Day05 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => input.Max(ToSeat);

        public override object SolvePart2(string[] input)
        {
            var seats = Enumerable.Range(0, 1024).ToList();
            foreach (var seat in input.Select(ToSeat))
            {
                seats.Remove(seat);
            }
            return seats.Where((seat, index) => seat != index).Min();
        }

        private static int ToSeat(string boardingPass)
        {
            var row = Convert.ToInt32(boardingPass[..7].Replace('B', '1').Replace('F', '0'), 2);
            var column = Convert.ToInt32(boardingPass[7..].Replace('R', '1').Replace('L', '0'), 2);
            return row * 8 + column;
        }
    }
}
