namespace AdventOfCode.Tests
{
    public class ExtensionMethodsTests
    {
        [Fact]
        public void CalculateFactors()
        {
            var number = 60;

            var factors = number.CalculateFactors();

            Assert.Equal(12, factors.Count);
            Assert.Equal([1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60], factors);
        }

        [Fact]
        public void PowerSet()
        {
            var numbers = Enumerable.Range(0, 3).ToArray();

            var powerSets = numbers.PowerSet().ToList();

            Assert.Equal(8, powerSets.Count);
            Assert.Contains([], powerSets);
            Assert.Contains([0], powerSets);
            Assert.Contains([1], powerSets);
            Assert.Contains([2], powerSets);
            Assert.Contains([0, 1], powerSets);
            Assert.Contains([0, 2], powerSets);
            Assert.Contains([1, 2], powerSets);
            Assert.Contains([0, 1, 2], powerSets);
        }

        [Fact]
        public void Permutations()
        {
            var numbers = Enumerable.Range(0, 3).ToArray();

            var permutations = numbers.Permutations().ToList();

            Assert.Equal(6, permutations.Count);
            Assert.Contains([0, 1, 2], permutations);
            Assert.Contains([0, 2, 1], permutations);
            Assert.Contains([1, 0, 2], permutations);
            Assert.Contains([1, 2, 0], permutations);
            Assert.Contains([2, 0, 1], permutations);
            Assert.Contains([2, 1, 0], permutations);
        }

        [Fact]
        public void Combinations()
        {
            var numbers = Enumerable.Range(0, 10).ToArray();

            var combinations = numbers.Combinations(2).ToList();

            Assert.Equal(45, combinations.Count);
            Assert.True(combinations.All(combination => combination.Length == 2));
            Assert.True(combinations.All(combination => combination[0] != combination[1]));
            Assert.Equal(9, combinations.Count(combination => combination[0] == 0));
            Assert.Equal(8, combinations.Count(combination => combination[0] == 1));
            Assert.Equal(7, combinations.Count(combination => combination[0] == 2));
            Assert.Equal(6, combinations.Count(combination => combination[0] == 3));
            Assert.Equal(5, combinations.Count(combination => combination[0] == 4));
            Assert.Equal(4, combinations.Count(combination => combination[0] == 5));
            Assert.Equal(3, combinations.Count(combination => combination[0] == 6));
            Assert.Equal(2, combinations.Count(combination => combination[0] == 7));
            Assert.Equal(1, combinations.Count(combination => combination[0] == 8));
            Assert.Equal(0, combinations.Count(combination => combination[0] == 9));
            Assert.Equal(0, combinations.Count(combination => combination[1] == 0));
            Assert.Equal(1, combinations.Count(combination => combination[1] == 1));
            Assert.Equal(2, combinations.Count(combination => combination[1] == 2));
            Assert.Equal(3, combinations.Count(combination => combination[1] == 3));
            Assert.Equal(4, combinations.Count(combination => combination[1] == 4));
            Assert.Equal(5, combinations.Count(combination => combination[1] == 5));
            Assert.Equal(6, combinations.Count(combination => combination[1] == 6));
            Assert.Equal(7, combinations.Count(combination => combination[1] == 7));
            Assert.Equal(8, combinations.Count(combination => combination[1] == 8));
            Assert.Equal(9, combinations.Count(combination => combination[1] == 9));
        }
    }
}
