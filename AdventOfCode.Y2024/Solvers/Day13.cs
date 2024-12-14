using Coords = AdventOfCode.Coords<long>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day13 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input) => ToClawMachines(input).Sum(machine => CalculateTokens(machine, 0L));
        public override object SolvePart2(string[] input) => ToClawMachines(input).Sum(machine => CalculateTokens(machine, 10_000_000_000_000));

        private static long CalculateTokens(ClawMachine machine, long offset = 0)
        {
            var Ax = Convert.ToDouble(machine.ButtonA.X);
            var Bx = machine.ButtonB.X;
            var Px = machine.Prize.X + offset;
            var Ay = machine.ButtonA.Y;
            var By = machine.ButtonB.Y;
            var Py = machine.Prize.Y + offset;
            /* Math:
             *  1) a * Ax + b * Bx = Px
             *  2) a * Ax = Px - b * Bx                                     1) - b * Bx
             *  3) a = (Px - b * Bx) / Ax                                   2) / Ax
             *  4) a * Ay + b * By = Py
             *  5) ((Px - b * Bx) / Ax) * Ay + b * By = Py                  4) with 'a' replaced by 3)
             *  6) (Px / Ax - b * Bx / Ax) * Ay + b * By = Py               5) / Ax split over substraction ║ (60 - 40) / 20 == 60 / 20 - 40 / 20
             *  7) Px * Ay / Ax - (b * Bx * Ay) / Ax + b * By = Py          6) * Ay worked into division
             *  8) - (b * Bx * Ay) / Ax + b * By = Py - Px * Ay / Ax        7) - (Px * Ay / Ax)
             *  9) b * By - (b * Bx * Ay) / Ax = Py - Px * Ay / Ax          8) commutative property of +
             * 10) b * (By - Bx * Ay / Ax) = Py - Px * Ay / Ax              9) merge 'b's ║ (b * Bx * Ay) / Ax == b * (Bx * Ay / Ax)
             * 11) b = (Py - Px * Ay / Ax) / (By - Bx * Ay / Ax)            10) / By - Bx * Ay / Ax
             */
            var b = (Py - Px * Ay / Ax) / (By - Bx * Ay / Ax); // 11) from above
            var a = (Px - b * Bx) / Ax; // 4) from above
            var longA = (long)Math.Round(a);
            var longB = (long)Math.Round(b);
            if (longA * Ax + longB * Bx == Px && longA * Ay + longB * By == Py)
            {
                return longA * 3 + longB;
            }
            return 0L;
        }

        private static List<ClawMachine> ToClawMachines(string[] groups)
        {
            var machines = new List<ClawMachine>();
            for (int i = 0; i < groups.Length; i++)
            {
                var lines = groups[i].SplitIntoLines();
                var (left, right) = lines[0][12..].SplitInTwo(", Y+");
                var offsetA = new Coords(int.Parse(left), int.Parse(right));
                (left, right) = lines[1][12..].SplitInTwo(", Y+");
                var offsetB = new Coords(int.Parse(left), int.Parse(right));
                (left, right) = lines[2][9..].SplitInTwo(", Y=");
                var offsetPrize = new Coords(int.Parse(left), int.Parse(right));
                machines.Add(new(i, offsetA, offsetB, offsetPrize));
            }
            return machines;
        }

        private record class ClawMachine(int Index, Coords ButtonA, Coords ButtonB, Coords Prize);
    }
}
