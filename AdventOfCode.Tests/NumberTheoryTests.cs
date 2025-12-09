namespace AdventOfCode.Tests
{
    public class NumberTheoryTests
    {
        [Fact]
        public void GCD()
        {
            int[] numbers = [18, 48];

            var gcd = NumberTheory.GCD(numbers);

            Assert.Equal(6L, gcd);
        }

        [Fact]
        public void LCM()
        {
            int[] numbers = [2, 3, 4, 5, 7];

            var lcm = NumberTheory.LCM(numbers);

            Assert.Equal(420L, lcm);
        }
    }
}
