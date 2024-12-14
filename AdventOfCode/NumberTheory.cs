namespace AdventOfCode
{
    public static class NumberTheory
    {
        #region Greatest common divisor (GCD)
        // https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// <summary>Greatest common divisor</summary>
        private static int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);
        /// <summary>Greatest common divisor</summary>
        public static long GCD(params int[] numbers) => numbers.Aggregate(GCD);
        /// <summary>Greatest common divisor</summary>
        public static long GCD(IEnumerable<int> numbers) => numbers.Aggregate(GCD);
        /// <summary>Greatest common divisor</summary>
        private static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);
        /// <summary>Greatest common divisor</summary>
        public static long GCD(params long[] numbers) => numbers.Aggregate(GCD);
        /// <summary>Greatest common divisor</summary>
        public static long GCD(IEnumerable<long> numbers) => numbers.Aggregate(GCD);
        #endregion

        #region Least common multiple (LCM)
        // https://en.wikipedia.org/wiki/Least_common_multiple
        /// <summary>Least common multiple</summary>
        private static int LCM(int a, int b) => (a * b) / GCD(a, b);
        /// <summary>Least common multiple</summary>
        public static long LCM(params int[] numbers) => numbers.Aggregate(LCM);
        /// <summary>Least common multiple</summary>
        public static long LCM(IEnumerable<int> numbers) => numbers.Aggregate(LCM);
        /// <summary>Least common multiple</summary>
        private static long LCM(long a, long b) => (a * b) / GCD(a, b);
        /// <summary>Least common multiple</summary>
        public static long LCM(params long[] numbers) => numbers.Aggregate(LCM);
        /// <summary>Least common multiple</summary>
        public static long LCM(IEnumerable<long> numbers) => numbers.Aggregate(LCM);
        #endregion
    }
}
