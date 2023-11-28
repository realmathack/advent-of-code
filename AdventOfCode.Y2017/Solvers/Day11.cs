namespace AdventOfCode.Y2017.Solvers
{
    public class Day11 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var current = new Position(0, 0);
            foreach (var direction in input.Split(','))
            {
                current.Move(direction);
            }
            return (Math.Abs(current.Col) + Math.Abs(current.Row)) / 2;
        }

        public override object SolvePart2(string input)
        {
            var furthest = 0;
            var current = new Position(0, 0);
            foreach (var direction in input.Split(','))
            {
                current.Move(direction);
                var distance = (Math.Abs(current.Col) + Math.Abs(current.Row)) / 2;
                if (distance > furthest)
                {
                    furthest = distance;
                }
            }
            return furthest;
        }

        private record struct Position(int Col, int Row)
        {
            public void Move(string direction)
            {
                switch (direction)
                {
                    case "n":
                        Row++;
                        Row++;
                        break;
                    case "s":
                        Row--;
                        Row--;
                        break;
                    case "nw":
                        Row++;
                        Col--;
                        break;
                    case "se":
                        Row--;
                        Col++;
                        break;
                    case "ne":
                        Row++;
                        Col++;
                        break;
                    case "sw":
                        Row--;
                        Col--;
                        break;
                }
            }
        }
    }
}
