namespace AdventOfCode.Y2025.Solvers
{
    public class Day01 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var count = 0;
            var current = 50;
            foreach (var line in input)
            {
                current = (current + ParseDelta(line)) % 100;
                if (current == 0)
                {
                    count++;
                }
            }
            return count;
        }

        public override object SolvePart2(string[] input)
        {
            var count = 0;
            var current = 50;
            foreach (var line in input)
            {
                var delta = ParseDelta(line);
                count += Math.Abs(delta / 100); // Rotations
                delta %= 100;
                current += delta;
                if (current <= 0 || current >= 100)
                {
                    var movedPassedZero = (current != delta) ? 1 : 0;
                    count += movedPassedZero;
                }
                current = (100 + current) % 100;
            }
            return count;
        }

        private static int ParseDelta(string line) => int.Parse((line[0] == 'L' ? "-" : "") + line[1..]);
    }
}
