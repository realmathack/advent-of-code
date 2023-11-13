namespace AdventOfCode.Y2017.Solvers
{
    public class Day11 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var current = (Col: 0, Row: 0);
            foreach (var direction in input.Split(','))
            {
                switch (direction)
                {
                    case "n":
                        current.Row++;
                        current.Row++;
                        break;
                    case "s":
                        current.Row--;
                        current.Row--;
                        break;
                    case "nw":
                        current.Row++;
                        current.Col--;
                        break;
                    case "se":
                        current.Row--;
                        current.Col++;
                        break;
                    case "ne":
                        current.Row++;
                        current.Col++;
                        break;
                    case "sw":
                        current.Row--;
                        current.Col--;
                        break;
                }
            }
            return (Math.Abs(current.Col) + Math.Abs(current.Row)) / 2;
        }

        public override object SolvePart2(string input)
        {
            var furthest = 0;
            var current = (Col: 0, Row: 0);
            foreach (var direction in input.Split(','))
            {
                switch (direction)
                {
                    case "n":
                        current.Row++;
                        current.Row++;
                        break;
                    case "s":
                        current.Row--;
                        current.Row--;
                        break;
                    case "nw":
                        current.Row++;
                        current.Col--;
                        break;
                    case "se":
                        current.Row--;
                        current.Col++;
                        break;
                    case "ne":
                        current.Row++;
                        current.Col++;
                        break;
                    case "sw":
                        current.Row--;
                        current.Col--;
                        break;
                }
                var distance = (Math.Abs(current.Col) + Math.Abs(current.Row)) / 2;
                if (distance > furthest)
                {
                    furthest = distance;
                }
            }
            return furthest;
        }
    }
}
