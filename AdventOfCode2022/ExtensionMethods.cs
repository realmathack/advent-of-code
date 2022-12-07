namespace AdventOfCode2022
{
    internal static class ExtensionMethods
    {
        public static string[] SplitIntoLines(this string input)
        {
            return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitIntoSections(this string input)
        {
            return input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
