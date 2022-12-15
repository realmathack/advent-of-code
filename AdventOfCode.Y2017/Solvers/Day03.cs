namespace AdventOfCode.Y2017.Solvers
{
    public class Day03 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var target = int.Parse(input);
            if (target == 1) return 0;
            var ringFromCenter = 0;
            var sideLength = 1;
            while ((sideLength * sideLength) < target)
            {
                ringFromCenter++;
                sideLength += 2;
            }
            var max = sideLength * sideLength;
            for (int side = 0; side < 4; side++)
            {
                for (int i = 0; i < sideLength - 1; i++)
                {
                    if (target == max - side * (sideLength - 1) - i)
                    {
                        return ringFromCenter + Math.Abs(i - ringFromCenter);
                    }
                }
            }
            return -1;
        }

        public override object SolvePart2(string input)
        {
            var target = int.Parse(input);
            var current = new Coords(0, 0);
            var direction = Coords.OffsetRight;
            var values = new Dictionary<Coords, int> { { current, 1 } };
            for (int length = 1; ; length++)
            {
                for (int side = 0; side < 2; side++)
                {
                    for (int i = 0; i < length; i++)
                    {
                        current += direction;
                        var sum = 0;
                        foreach (var adjacent in current.Adjacents)
                        {
                            if (values.TryGetValue(adjacent, out var value))
                            {
                                sum += value;
                            }
                        }
                        if (sum > target)
                        {
                            return sum;
                        }
                        values.Add(current, sum);
                    }
                    direction = direction.RotateLeft;
                }
            }
        }
    }
}
