using System.Numerics;
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

        public static (string Left, string Right) SplitInTwo(this string input, char separator)
        {
            var parts = input.Split(separator, 2);
            return (parts[0], parts[1]);
        }

        public static (string Left, string Right) SplitInTwo(this string input, string separator)
        {
            var parts = input.Split(separator, 2);
            return (parts[0], parts[1]);
        }

        public static char[][] ToCharGrid(this string[] input) => input.Select(line => line.ToCharArray()).ToArray();
        public static int[][] ToNumberGrid(this string[] input) => input.Select(line => line.Select(cell => cell - '0').ToArray()).ToArray();
        public static bool[][] ToBoolGrid(this string[] input, Func<char, bool> selector) => input.Select(line => line.Select(selector).ToArray()).ToArray();
        public static bool IsOutOfBounds<T>(this T[][] grid, Coords coords) => (coords.X < 0 || coords.Y < 0 || coords.Y >= grid.Length || coords.X >= grid[coords.Y].Length);

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

        public static IEnumerable<IList<T>> Permutations<T>(this IList<T> input, int indexFrom = 0)
        {
            if (indexFrom + 1 == input.Count)
            {
                yield return input;
            }
            else
            {
                foreach (var permutation in Permutations(input, indexFrom + 1))
                {
                    yield return permutation;
                }
                for (var i = indexFrom + 1; i < input.Count; i++)
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

        public static string ToMD5Hex(this string input) => Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(input))).ToLower();
    }
}
