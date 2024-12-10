namespace AdventOfCode.Y2016.Solvers
{
    public class Day03 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var triangles = 0;
            foreach (var line in input)
            {
                var sides = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                if (sides[0] + sides[1] > sides[2] && sides[2] + sides[0] > sides[1] && sides[1] + sides[2] > sides[0])
                {
                    triangles++;
                }
            }
            return triangles;
        }

        public override object SolvePart2(string[] input)
        {
            var triangles = 0;
            var sides = input.SelectMany(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).ToArray();
            for (int i = 0; i < sides.Length; i += 9)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sides[i + j] + sides[i + j + 3] > sides[i + j + 6] &&
                        sides[i + j + 6] + sides[i + j] > sides[i + j + 3] &&
                        sides[i + j + 3] + sides[i + j + 6] > sides[i + j])
                    {
                        triangles++;
                    }
                }
            }
            return triangles;
        }
    }
}
