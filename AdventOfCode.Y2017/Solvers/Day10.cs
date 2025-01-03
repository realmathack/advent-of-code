﻿namespace AdventOfCode.Y2017.Solvers
{
    public class Day10(int elementCount) : SolverWithText
    {
        public Day10() : this(256) { }

        public override object SolvePart1(string input)
        {
            var pos = 0;
            var skipSize = 0;
            var elements = Enumerable.Range(0, elementCount).ToArray();
            foreach (var length in input.Split(',').Select(int.Parse))
            {
                var end = pos + length;
                var subset = (end < elementCount) ? elements[pos..end] : [.. elements[pos..], .. elements[..(end % elementCount)]];
                subset = subset.Reverse().ToArray();
                for (int i = 0; i < subset.Length; i++)
                {
                    elements[(pos + i) % elementCount] = subset[i];
                }
                pos = (end + skipSize++) % elementCount;
            }
            return elements[0] * elements[1];
        }

        public override object SolvePart2(string input)
        {
            var lengths = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                lengths.Add(input[i]);
            }
            lengths.AddRange([17, 31, 73, 47, 23]);

            var pos = 0;
            var skipSize = 0;
            var elements = Enumerable.Range(0, elementCount).ToArray();
            for (int j = 0; j < 64; j++)
            {
                foreach (var length in lengths)
                {
                    var end = pos + length;
                    var subset = (end < elementCount) ? elements[pos..end] : [.. elements[pos..], .. elements[..(end % elementCount)]];
                    subset = subset.Reverse().ToArray();
                    for (int i = 0; i < subset.Length; i++)
                    {
                        elements[(pos + i) % elementCount] = subset[i];
                    }
                    pos = (end + skipSize++) % elementCount;
                }
            }

            var dense = new int[16];
            for (int j = 0; j < 16; j++)
            {
                var hash = elements[j * 16];
                for (int i = 1; i < 16; i++)
                {
                    hash ^= elements[j * 16 + i];
                }
                dense[j] = hash;
            }

            return string.Concat(dense.Select(number => number.ToString("X2"))).ToLower();
        }
    }
}
