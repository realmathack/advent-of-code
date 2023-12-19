namespace AdventOfCode
{
    public static class NumberTheory
    {
        public static long LeastCommonMultiple(params int[] numbers) => numbers.Aggregate(LCM);
        public static long LeastCommonMultiple(IEnumerable<int> numbers) => numbers.Aggregate(LCM);
        public static long LeastCommonMultiple(params long[] numbers) => numbers.Aggregate(LCM);
        public static long LeastCommonMultiple(IEnumerable<long> numbers) => numbers.Aggregate(LCM);

        private static int LCM(int a, int b)
        {
            return (a * b) / GCD(a, b);
        }

        private static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        public static long GreatestCommonDivisor(params int[] numbers) => numbers.Aggregate(GCD);
        public static long GreatestCommonDivisor(IEnumerable<int> numbers) => numbers.Aggregate(GCD);
        public static long GreatestCommonDivisor(params long[] numbers) => numbers.Aggregate(GCD);
        public static long GreatestCommonDivisor(IEnumerable<long> numbers) => numbers.Aggregate(GCD);

        private static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}
