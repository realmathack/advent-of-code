﻿namespace AdventOfCode.Y2017.Solvers
{
    public class Day02 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var checksum = 0;
            foreach (var line in input)
            {
                var numbers = line.Split('\t').Select(int.Parse);
                checksum += numbers.Max() - numbers.Min();
            }
            return checksum;
        }

        public override object SolvePart2(string[] input)
        {
            var sum = 0;
            foreach (var line in input)
            {
                var found = false;
                var numbers = line.Split('\t').Select(int.Parse);
                foreach (var high in numbers.OrderByDescending(number => number))
                {
                    foreach (var low in numbers.OrderBy(number => number))
                    {
                        if (low >= high)
                        {
                            continue;
                        }
                        if (high % low == 0)
                        {
                            sum += high / low;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }
            return sum;
        }
    }
}
