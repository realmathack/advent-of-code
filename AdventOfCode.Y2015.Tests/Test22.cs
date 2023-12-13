namespace AdventOfCode.Y2015.Tests
{
    public class Test22
    {
        [Theory]
        [InlineData(10, 250, _input1, 226)]
        [InlineData(10, 250, _input2, 641)]
        public void TestPart1(int playerHitPoints, int playerMana, string input, int expected)
        {
            var subject = new Day22(playerHitPoints, playerMana);
            subject.SetInput(input);

            var result = subject.SolvePart1();

            Assert.Equal(expected, result);
        }

        private const string _input1 = @"Hit Points: 13
Damage: 8
";
        private const string _input2 = @"Hit Points: 14
Damage: 8
";
    }
}
