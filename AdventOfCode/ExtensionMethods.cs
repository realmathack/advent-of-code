using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode
{
    public static class ExtensionMethods
    {
        // Strings
        public static string[] SplitIntoLines(this string input) => input.TrimEnd(Environment.NewLine.ToCharArray()).Split(Environment.NewLine);

        public static (string Left, string Right) SplitInTwo(this string input, char separator)
        {
            var pos = input.AsSpan().IndexOf(separator);
            return (input[0..pos], input[(pos + 1)..]);
        }

        public static (string Left, string Right) SplitInTwo(this string input, string separator)
        {
            var pos = input.AsSpan().IndexOf(separator);
            return (input[0..pos], input[(pos + separator.Length)..]);
        }

        // Grids
        public static bool IsOutOfBounds<T>(this T[][] grid, Coords<int> coords) => coords.Y < 0 || coords.X < 0 || coords.Y >= grid.Length || coords.X >= grid[coords.Y].Length;

        // Math
        #region Product (copied from Enumerable.Sum)
        public static int Product(this IEnumerable<int> source) => Product<int, int>(source);
        public static long Product(this IEnumerable<long> source) => Product<long, long>(source);
        private static TResult Product<TSource, TResult>(this IEnumerable<TSource> source)
            where TSource : struct, INumber<TSource>
            where TResult : struct, INumber<TResult>
        {
            TResult product = TResult.One;
            foreach (TSource value in source)
            {
                checked { product *= TResult.CreateChecked(value); }
            }
            return product;
        }

        public static int Product<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) => Product<TSource, int, int>(source, selector);
        public static long Product<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector) => Product<TSource, long, long>(source, selector);
        private static TResult Product<TSource, TResult, TAccumulator>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
            where TResult : struct, INumber<TResult>
            where TAccumulator : struct, INumber<TAccumulator>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            TAccumulator product = TAccumulator.One;
            foreach (TSource value in source)
            {
                checked { product *= TAccumulator.CreateChecked(selector(value)); }
            }
            return TResult.CreateTruncating(product);
        }
        #endregion

        public static HashSet<int> CalculateFactors(this int input)
        {
            var factors = new HashSet<int>() { 1, input };
            for (int i = 2; i * i <= input; i++)
            {
                if (input % i == 0)
                {
                    factors.Add(i);
                    if (i * i != input)
                    {
                        factors.Add(input / i);
                    }
                }
            }
            return factors;
        }

        // Sets
        public static IEnumerable<List<T>> PowerSet<T>(this T[] input)
        {
            var powerSetSize = (int)Math.Pow(2, input.Length);
            for (int mask = 0; mask < powerSetSize; mask++)
            {
                var set = new List<T>(input.Length);
                for (int i = 0; i < input.Length; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        set.Add(input[i]);
                    }
                }
                yield return set;
            }
        }

        public static IEnumerable<T[]> Permutations<T>(this T[] input, int indexFrom = 0)
        {
            if (indexFrom + 1 == input.Length)
            {
                yield return input;
            }
            else
            {
                foreach (var permutation in Permutations(input, indexFrom + 1))
                {
                    yield return permutation;
                }
                for (var i = indexFrom + 1; i < input.Length; i++)
                {
                    (input[indexFrom], input[i]) = (input[i], input[indexFrom]);
                    foreach (var permutation in Permutations(input, indexFrom + 1))
                    {
                        yield return permutation;
                    }
                    (input[indexFrom], input[i]) = (input[i], input[indexFrom]);
                }
            }
        }

        // MD5
        public static string ToMD5Hex(this string input) => Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(input))).ToLower();
    }
}
