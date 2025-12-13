namespace AdventOfCode
{
    public interface ISolver
    {
        public object SolvePart1();
        public object SolvePart2();
        public void SetInput(string input, bool isRealInput);
    }
}
