namespace AdventOfCode
{
    public static class NumberTheory
    {
        public static long LeastCommonMultiple(params long[] numbers)
        {
            return numbers.Aggregate(LCM);
        }

        private static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        public static long GreatestCommonDivisor(params long[] numbers)
        {
            return numbers.Aggregate(GCD);
        }

        private static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}
