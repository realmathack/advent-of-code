using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode
{
    public static class ExtensionMethods
    {
        public static string[] SplitIntoLines(this string input)
        {
            return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitIntoSections(this string input)
        {
            return input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToMD5Hex(this string input)
        {
            return Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(input))).ToLower();
        }

        public static IEnumerable<IList<T>> PowerSet<T>(this IList<T> input)
        {
            var powerSetSize = (int)Math.Pow(2, input.Count);
            for (int mask = 0; mask < powerSetSize; mask++)
            {
                var set = new List<T>();
                for (int i = 0; i < input.Count; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        set.Add(input[i]);
                    }
                }
                yield return set;
            }
        }

        public static IEnumerable<T[]> Permutations<T>(this T[] values, int indexFrom = 0)
        {
            if (indexFrom + 1 == values.Length)
            {
                yield return values;
            }
            else
            {
                foreach (var permutation in Permutations(values, indexFrom + 1))
                {
                    yield return permutation;
                }
                for (var i = indexFrom + 1; i < values.Length; i++)
                {
                    (values[indexFrom], values[i]) = (values[i], values[indexFrom]);
                    foreach (var permutation in Permutations(values, indexFrom + 1))
                    {
                        yield return permutation;
                    }
                    (values[indexFrom], values[i]) = (values[i], values[indexFrom]);
                }
            }
        }
    }
}
