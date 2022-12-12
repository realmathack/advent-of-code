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

        public static IEnumerable<IEnumerable<T>> PowerSet<T>(this List<T> input)
        {
            var powerSetSize = (int)Math.Pow(2, input.Count);
            for (var mask = 0; mask < powerSetSize; mask++)
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

        public static void SetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
            where TKey : notnull
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return;
            }
            dictionary.Add(key, value);
        }
    }
}
